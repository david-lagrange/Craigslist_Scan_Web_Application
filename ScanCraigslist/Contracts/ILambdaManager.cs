using ScanCraigslist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScanCraigslist.Contracts
{
    public interface ILambdaManager
    {
        Task<List<CraigslistListing>> GetCraigslist(LambdaEvent lambdaEvent);
        Task<HttpResponseMessage> RunLambda(string awsEvent);
    }
}
