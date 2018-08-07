using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BPNewMVCTest1.Models;
using BPNewMVCTest1Service.TokenDTService;
using Microsoft.Extensions.Configuration;
using BPNewMVCTest1Service.HttpService;
using System.Net.Http;

namespace BPNewMVCTest1.Controllers
{
    public class HomeController : BaseController
    {
        private ITokenDTService _tokenDTService;
        public IConfiguration _configuration;
        private IHttpService _httpService;
        public HomeController(ITokenDTService tokenDTService, IConfiguration configuration, IHttpService httpService)
                :base(tokenDTService, configuration)
        {
            _tokenDTService = tokenDTService;
            _configuration = configuration;
            _httpService = httpService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "Your application description page.";

            HttpClient client = _httpService.GetHttpClientInstance();
            HttpResponseMessage response = await client.GetAsync(_httpService.GetBaseURL() + "dashboard/home");
            HttpResponseMessage response1 = await client.GetAsync(_httpService.GetBaseURL() + "values");

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
