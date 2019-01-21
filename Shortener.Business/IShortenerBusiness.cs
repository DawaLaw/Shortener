using Shortener.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Business
{
    public interface IShortenerBusiness
    {
        void VerifyUrl(UrlShortener urlShortener);
        Task AddUrl(UrlShortener urlShortener);
        Task<string> GenerateNewUrlId();
    }
}
