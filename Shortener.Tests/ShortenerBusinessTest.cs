using Moq;
using Moq.Protected;
using Shortener.Business;
using Shortener.Models;
using Shortener.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shortener.Tests
{
    public class ShortenerBusinessTest
    {
        [Fact]
        public void VerifyUrlHttpTest_PassTest()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var shortenerBusiness = new ShortenerBusiness(mock.Object);

            // Action
            var outcome = Record.Exception(() => shortenerBusiness.VerifyUrl(MockGetHttpLongUrl()));

            // Assert
            Assert.Null(outcome);
        }

        [Fact]
        public void VerifyUrlHttpsTest_PassTest()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var shortenerBusiness = new ShortenerBusiness(mock.Object);

            // Action
            var outcome = Record.Exception(() => shortenerBusiness.VerifyUrl(MockGetHttpsLongUrl()));

            // Assert
            Assert.Null(outcome);
        }

        [Fact]
        public void VerifyUrlTestInvalid_FailTest()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var shortenerBusiness = new ShortenerBusiness(mock.Object);
            var urlShortener = new UrlShortener
            {
                LongUrl = "http:/abc.com"
            };

            // Action
            var outcome = Assert.Throws<Exception>(() => shortenerBusiness.VerifyUrl(urlShortener));

            // Assert
            Assert.Equal("Url is invalid or unreacheable.", outcome.Message);
        }

        [Fact]
        public void VerifyUrlTestUnreacheable_FailTest()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var shortenerBusiness = new ShortenerBusiness(mock.Object);
            var urlShortener = new UrlShortener
            {
                LongUrl = "http://123123123123123123123123123123.com"
            };

            // Action
            var outcome = Assert.Throws<Exception>(() => shortenerBusiness.VerifyUrl(urlShortener));

            // Assert
            Assert.Equal("Url is invalid or unreacheable.", outcome.Message);
        }

        [Fact]
        public async Task GenerateNewUrlId_PassTest()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var shortenerBusiness = new ShortenerBusiness(mock.Object);

            // Action
            var outcome = await shortenerBusiness.GenerateNewUrlId();

            // Assert
            Assert.Equal(7, outcome.Length);
        }

        [Fact]
        public async Task GenerateNewUrlId_FailTest()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(storage => storage.GetUrl("1234567"))
                .ReturnsAsync(MockGetLongUrlEntity());

            var mockShortenerBusiness = new Mock<ShortenerBusiness>(mock.Object);
            mockShortenerBusiness.Protected().Setup<string>("GenerateUrlId").Returns("1234567");

            // Action
            var outcome = await mockShortenerBusiness.Object.GenerateNewUrlId();

            // Assert
            Assert.Null(outcome);
        }

        [Fact]
        public async Task GetUrlTest_PassTest()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(storage => storage.GetUrl("1234567"))
                .ReturnsAsync(MockGetLongUrlEntity());
            var shortenerBusiness = new ShortenerBusiness(mock.Object);

            // Action
            var outcome = await shortenerBusiness.GetUrl(MockGetUrlId());

            // Assert
            Assert.Equal("https://original.long.url/with/path/and/index.html", outcome.LongUrl);
        }

        [Fact]
        public async Task GetUrlTestInvalid_FailTest()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var shortenerBusiness = new ShortenerBusiness(mock.Object);

            // Action
            var outcome = await Assert.ThrowsAsync<Exception>(() => shortenerBusiness.GetUrl(MockGetUrlId()));

            // Assert
            Assert.Equal("Invalid Short Url provided.", outcome.Message);
        }

        private static UrlShortener MockGetHttpLongUrl()
        {
            return new UrlShortener
            {
                LongUrl = "http://facebook.com"
            };
        }

        private static UrlShortener MockGetHttpsLongUrl()
        {
            return new UrlShortener
            {
                LongUrl = "https://facebook.com"
            };
        }

        private object MockGetLongUrlEntity()
        {
            var shortenerEntity = new UrlShortenerEntity("1", "1234567")
            {
                LongUrl = "https://original.long.url/with/path/and/index.html"
            };
            return shortenerEntity;
        }

        private static UrlShortener MockGetUrlId()
        {
            return new UrlShortener
            {
                UrlId = "1234567"
            };
        }
    }
}
