using HANS.NetCore.Jwt.Interface;
using HANS.NetCore.Jwt.Sample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace HANS.NetCore.Jwt.Sample.Controllers
{
    public class HomeController : Controller
    {
        private IJsonWebTokenBuilder jsonWebTokenBuilder { get; set; }

        public HomeController(IJsonWebTokenBuilder jsonWebTokenBuilder)
        {
            this.jsonWebTokenBuilder = jsonWebTokenBuilder;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "common")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CreateJsonToken(Dictionary<string, string> payLoad)
        {
            return Content(jsonWebTokenBuilder.CreateJsonWebToken(payLoad));
        }
    }
}