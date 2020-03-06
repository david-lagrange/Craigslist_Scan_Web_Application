using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScanCraigslist.Contracts;
using ScanCraigslist.Models;

namespace ScanCraigslist.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILambdaManager _lambdaManager;

        public HomeController(ILogger<HomeController> logger, ILambdaManager lambdaManager)
        {
            _logger = logger;
            _lambdaManager = lambdaManager;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel() { Error = "" });
        }
        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel viewModel)
        {
            try
            {
                List<CraigslistListing> listings = await _lambdaManager.GetCraigslist(new LambdaEvent() { search_query = viewModel.Search.KeyWords, category = viewModel.Search.Category, amount_of_lists = "100" });
                viewModel.Listings = listings.Where(l => !l.title.ToLower().Contains("wanted") && !l.title.ToLower().Contains("collect")).ToList();
                return View(viewModel);
            }
            catch(Exception e)
            {
                viewModel.Error = e.Message;
                return View(viewModel);
            }

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
