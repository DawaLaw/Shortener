using Shortener.Models;
using System;
using System.Threading.Tasks;

namespace Shortener.Business
{
    public class ShortenerFacade : IShortenerFacade
    {
        private readonly IShortenerBusiness _shortenerBusiness;

        public ShortenerFacade(IShortenerBusiness shortenerBusiness)
        {
            _shortenerBusiness = shortenerBusiness;
        }

        /// <summary>
        /// Generate Short Url and save in repository
        /// </summary>
        /// <param name="urlShortener"></param>
        public async Task GenerateShortUrl(UrlShortener urlShortener)
        {
            // Verify if the URL exists or reacheable
            _shortenerBusiness.VerifyUrl(urlShortener);

            // Generate New ID
            urlShortener.UrlId = await _shortenerBusiness.GenerateNewUrlId();

            // Add to Repository
            await _shortenerBusiness.AddUrl(urlShortener);
        }
    }
}
