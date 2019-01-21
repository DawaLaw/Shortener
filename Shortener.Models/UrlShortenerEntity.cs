using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shortener.Models
{
    public class UrlShortenerEntity : TableEntity
    {
        public UrlShortenerEntity(string firstChar, string UrlId)
        {
            this.PartitionKey = firstChar;
            this.RowKey = UrlId;
        }

        public UrlShortenerEntity()
        {

        }

        public string LongUrl { get; set; }

    }
}
