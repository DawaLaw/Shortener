using Shortener.Models;
using System;

namespace Shortener.Business
{
    public class ShortenerFacade : IShortenerFacade
    {
        /// <summary>
        /// Generate Short Url and save in repository
        /// </summary>
        /// <param name="urlShortener"></param>
        public void GenerateShortUrl(UrlShortener urlShortener)
        {
            // Verify if the URL exists or reacheable
            
            // Add to Repository
        }
    }
}
