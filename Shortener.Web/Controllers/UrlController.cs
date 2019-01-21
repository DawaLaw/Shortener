using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shortener.Business;
using Shortener.Models;

namespace Shortener.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IShortenerFacade _shortenerFacade;
        public UrlController(IShortenerFacade shortenerFacade)
        {
            _shortenerFacade = shortenerFacade;
        }

        [HttpPost]
        public async Task<string> Post([FromBody] string longUrl)
        {
            try
            {
                var shortener = new UrlShortener
                {
                    LongUrl = longUrl
                };
                await _shortenerFacade.GenerateShortUrl(shortener);
                shortener.ShortUrl = $"{Request.Scheme}://{Request.Host}/{shortener.UrlId}";

                return shortener.ShortUrl;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
