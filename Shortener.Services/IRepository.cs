using Shortener.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Services
{
    public interface IRepository
    {
        Task AddUrl(UrlShortener urlShortener);
    }
}
