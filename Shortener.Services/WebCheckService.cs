using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Shortener.Services
{
    public static class WebCheckService
    {
        /// <summary>
        /// Check if the URL is reacheable and returning 200 status code.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>IsReacheable</returns>
        public static bool IsUrlReacheable(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Head;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
