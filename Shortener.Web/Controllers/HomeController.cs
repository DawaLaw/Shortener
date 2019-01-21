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
        public async Task<IActionResult> Index(UrlShortener urlShortener)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _shortenerFacade.GenerateShortUrl(urlShortener);
                    urlShortener.ShortUrl = $"{Request.Scheme}://{Request.Host}/{urlShortener.UrlId}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View(urlShortener);
        }

        public async Task<IActionResult> Go(string urlId)
        {
            try
            {
                var urlShortener = new UrlShortener() { UrlId = urlId };
                urlShortener = await _shortenerFacade.GetUrl(urlShortener);
                return RedirectPermanent(urlShortener.LongUrl);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View("Index", new UrlShortener());
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
