using System;
using System.ComponentModel.DataAnnotations;

namespace Shortener.Models
{
    public class UrlShortener
    {
        [Url]
        [Display(Name = "Shortened URL")]
        public string ShortUrl { get; set; }

        [Url]
        [Required]
        [Display(Name = "Long URL")]
        public string LongUrl { get; set; }
    }
}
