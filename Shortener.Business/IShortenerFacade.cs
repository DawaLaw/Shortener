using Shortener.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Business
{
    public interface IShortenerFacade
    {
        Task GenerateShortUrl(UrlShortener urlShortener);
        Task<UrlShortener> GetUrl(UrlShortener urlShortener);
    }
}
