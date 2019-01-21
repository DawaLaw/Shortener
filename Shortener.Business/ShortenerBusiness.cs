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
    }
}
