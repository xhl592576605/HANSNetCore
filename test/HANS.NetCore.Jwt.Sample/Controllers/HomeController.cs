using HANS.NetCore.Jwt.Interface;
using HANS.NetCore.Jwt.Interface.Policy;
using HANS.NetCore.Jwt.Sample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace HANS.NetCore.Jwt.Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJsonWebTokenBuilder jsonWebTokenBuilder;

        public HomeController(IJsonWebTokenBuilder jsonWebTokenBuilder)
        {
            this.jsonWebTokenBuilder = jsonWebTokenBuilder;
        }

        [JwtAuthorizeAttribute(Policy = "common")]
        public IActionResult Index()
        {
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

        [HttpPost]
        public IActionResult CreateJsonToken(Dictionary<string, string> payLoad)
        {
            return Content(jsonWebTokenBuilder.CreateJsonWebToken(payLoad));
        }
    }
}