
using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScanCraigslist.Contracts;
using ScanCraigslist.Keys;
using ScanCraigslist.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScanCraigslist.Services
{
    public class LambdaManager : ILambdaManager
    {
        public DateTime lastCall;
        public string lastSearchCategory;
        public string lastSearchQuery;
        public List<CraigslistListing> lastListings;
        public LambdaManager()
        {
            lastCall = DateTime.Now.AddDays(-1);
            lastSearchCategory = "";
            lastSearchQuery = "";
            lastListings = new List<CraigslistListing>();
        }
        private AmazonLambdaClient lambdaClient = new AmazonLambdaClient(ApiKeys.lambdaKey, ApiKeys.lambdaSecret, RegionEndpoint.USEast2);
        public async Task<List<CraigslistListing>> GetCraigslist(LambdaEvent lambdaEvent)
        {
            if(lastCall < DateTime.Now.AddMinutes(-20) || lastSearchQuery != lambdaEvent.search_query || lastSearchCategory != lambdaEvent.category)
            {
                List<CraigslistListing> listings = new List<CraigslistListing>();
                string awsEvent = JsonConvert.SerializeObject(lambdaEvent);
                HttpResponseMessage response = await RunLambda(awsEvent);
                if (response.IsSuccessStatusCode)
                {
                    CraigslistListings allListings = JsonConvert.DeserializeObject<CraigslistListings>(response.Content.ReadAsStringAsync().Result);
                    foreach (CraigslistListing listing in allListings.listings)
                    {
                        listings.Add(listing);
                    }
                    lastListings = listings;
                    lastCall = DateTime.Now;
                    lastSearchQuery = lambdaEvent.search_query;
                    lastSearchCategory = lambdaEvent.category;
                    return listings;
                }
                return null;
            }
            else
            {
                return lastListings;
            }
            
        }

        public async Task<HttpResponseMessage> RunLambda(string awsEvent)
        {
            var lambdaRequest = new InvokeRequest
            {
                FunctionName = "scanCraigslist",
                Payload = awsEvent
            };

            var response = await lambdaClient.InvokeAsync(lambdaRequest);
            if (response != null)
            {
                using (var sr = new StreamReader(response.Payload))
                {
                    string result = await sr.ReadToEndAsync();
                    JObject jobject = JObject.Parse(result);
                    return new HttpResponseMessage()
                    {
                        StatusCode = jobject["statusCode"].ToString() == "200" ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                        Content = new StringContent(jobject["body"].ToString())
                    };
                }
            }
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("")
            }; ;

        }
    }
}
