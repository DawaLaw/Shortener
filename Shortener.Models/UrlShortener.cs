using System;
using System.ComponentModel.DataAnnotations;

namespace Shortener.Models
{
    public class UrlShortener
    {
        public string UrlId { get; set; }

        [Url]
        [Display(Name = "Shortened URL")]
        public string ShortUrl { get; set; }

        [Url]
        [Required]
        [Display(Name = "Long URL")]
        public string LongUrl { get; set; }

        public static implicit operator UrlShortener(UrlShortenerEntity urlShortenerEntity)
        {
            return new UrlShortener
            {
                UrlId = urlShortenerEntity.RowKey,
                LongUrl = urlShortenerEntity.LongUrl
            };
        }
    }
}
