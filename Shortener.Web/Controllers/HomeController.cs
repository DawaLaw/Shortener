using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shortener.Business;
using Shortener.Models;
using Shortener.Web.Models;

namespace Shortener.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShortenerFacade _shortenerFacade;

        public HomeController(IShortenerFacade shortenerFacade)
        {
            _shortenerFacade = shortenerFacade;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new UrlShortener());
        }

        [HttpPost]
        public IActionResult Index(UrlShortener urlShortener)
        {
            _shortenerFacade.GenerateShortUrl(urlShortener);
            urlShortener.ShortUrl = "https://localhost:53469/1234567";
            return View(urlShortener);
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
