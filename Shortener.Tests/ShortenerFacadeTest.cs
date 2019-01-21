using Moq;
using Shortener.Business;
using Shortener.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shortener.Tests
{
    public class ShortenerFacadeTest
    {
        [Fact]
        public async Task GenerateShortUrlTest_PassTest()
        {
            // Arrange
            var urlShortener = new UrlShortener();
            var mockShortenerBusiness = new Mock<IShortenerBusiness>();
            mockShortenerBusiness.Setup(biz => biz.GenerateNewUrlId()).ReturnsAsync("1234567");
            var urlShortener2 = new UrlShortener
            {
                UrlId = "1234567"
            };
            mockShortenerBusiness.Setup(biz => biz.AddUrl(urlShortener2));
            var shortenerFacade = new ShortenerFacade(mockShortenerBusiness.Object);

            // Action
            var outcome = await Record.ExceptionAsync(() => shortenerFacade.GenerateShortUrl(urlShortener));

            // Assert
            Assert.Null(outcome);
        }
    }
}
