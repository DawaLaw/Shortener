using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shortener.Business;
using Shortener.Models;
using Shortener.Web.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shortener.Tests
{
    public class HomeControllerTest
    {
        [Fact]
        public async Task IndexPostTest_PassTest()
        {
            // Arrange
            var mockShortenerFacade = new Mock<IShortenerFacade>();
            var controller = PrepareController(mockShortenerFacade);

            // Action
            var outcome = await controller.Index(MockGetLongUrlUrlId());

            // Assert
            var viewResult = Assert.IsType<ViewResult>(outcome);
            Assert.Equal("https://localhost:44317/1234567", ((UrlShortener)viewResult.Model).ShortUrl);
        }

        [Fact]
        public async Task IndexPostTest_FailTest()
        {
            // Arrange
            var urlShortener = MockGetLongUrlUrlId();
            var mockShortenerFacade = new Mock<IShortenerFacade>();
            mockShortenerFacade.Setup(facade => facade.GenerateShortUrl(It.IsAny<UrlShortener>()))
                .ThrowsAsync(new Exception("Url is invalid or unreacheable."));
            var controller = PrepareController(mockShortenerFacade);

            // Action
            var outcome = await controller.Index(urlShortener);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(outcome);
            Assert.Equal(urlShortener, ((UrlShortener)viewResult.Model));
            Assert.Equal("Url is invalid or unreacheable.", controller.ViewBag.ErrorMessage);
        }

        [Fact]
        public async Task GoTest_PassTest()
        {
            // Arrange
            var mockShortenerFacade = new Mock<IShortenerFacade>();
            mockShortenerFacade.Setup(facade => facade.GetUrl(It.IsAny<UrlShortener>())).ReturnsAsync(MockGetLongUrlUrlId());
            var controller = new HomeController(mockShortenerFacade.Object);

            // Action
            var outcome = await controller.Go("1234567");

            // Assert
            var redirectResult = Assert.IsType<RedirectResult>(outcome);
            Assert.True(redirectResult.Permanent);
            Assert.Equal("http://facebook.com", redirectResult.Url);
        }

        [Fact]
        public async Task GoTest_FailTest()
        {
            // Arrange
            var mockShortenerFacade = new Mock<IShortenerFacade>();
            mockShortenerFacade.Setup(facade => facade.GetUrl(It.IsAny<UrlShortener>()))
                .ThrowsAsync(new Exception("Invalid Short Url provided."));
            var controller = new HomeController(mockShortenerFacade.Object);

            // Action
            var outcome = await controller.Go("1234567");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(outcome);
            Assert.Equal("Invalid Short Url provided.", controller.ViewBag.ErrorMessage);
        }

        private static HomeController PrepareController(Mock<IShortenerFacade> mockShortenerFacade)
        {
            var controller = new HomeController(mockShortenerFacade.Object);
            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(req => req.Scheme).Returns("https");
            mockRequest.Setup(req => req.Host).Returns(new HostString("localhost", 44317));
            var mockContext = new Mock<HttpContext>();
            mockContext.Setup(con => con.Request).Returns(mockRequest.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockContext.Object
            };
            return controller;
        }

        private static UrlShortener MockGetLongUrlUrlId()
        {
            return new UrlShortener
            {
                LongUrl = "http://facebook.com",
                UrlId = "1234567"
            };
        }
    }
}
