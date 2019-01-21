using Shortener.Models;
using Shortener.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Business
{
    public class ShortenerBusiness : IShortenerBusiness
    {
        private readonly IRepository _repository;
        public ShortenerBusiness(IRepository shortenerTableStorage)
        {
            _repository = shortenerTableStorage;
        }

        /// <summary>
        /// Verify if Url is valid and reacheable.
        /// </summary>
        /// <param name="urlShortener"></param>
        public void VerifyUrl(UrlShortener urlShortener)
        {

        }

        /// <summary>
        /// Add Url to repository
        /// </summary>
        /// <param name="urlShortener"></param>
        public async Task AddUrl(UrlShortener urlShortener)
        {
            await _repository.AddUrl(urlShortener);
        }

        /// <summary>
        /// Generate new UrlId
        /// </summary>
        /// <returns>Generated UrlId</returns>
        public async Task<string> GenerateNewUrlId()
        {
            for (byte i = 0; i < 10; i++)
            {
                var generatedId = GenerateUrlId();
                var outcome = await _repository.GetUrl(generatedId);
                if (outcome == null)
                {
                    return generatedId;
                }
            }
            return null;
        }

        /// <summary>
        /// Generate UrlId
        /// </summary>
        /// <returns>Generated UrlId</returns>
        protected virtual string GenerateUrlId()
        {
            // TODO: To rethink a way to generate ID.
            return Guid.NewGuid().ToString().Substring(0, 7);
        }
    }
}
