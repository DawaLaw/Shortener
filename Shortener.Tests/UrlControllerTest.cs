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
    public class UrlontrollerTest
    {
        [Fact]
        public async Task PostTest_PassTest()
        {
            // Arrange
            var mockShortenerFacade = new Mock<IShortenerFacade>();
            mockShortenerFacade.Setup(facade => facade.GenerateShortUrl(It.IsAny<UrlShortener>()))
                .Returns(Task.FromResult(0))
                .Callback((UrlShortener x) => x.UrlId = "1234567");
            var controller = PrepareController(mockShortenerFacade);

            // Action
            var outcome = await controller.Post("http://facebook.com");

            // Assert
            Assert.Equal("https://localhost:44317/1234567", outcome);
        }

        [Fact]
        public async Task PostTest_FailTest()
        {
            // Arrange
            var mockShortenerFacade = new Mock<IShortenerFacade>();
            mockShortenerFacade.Setup(facade => facade.GenerateShortUrl(It.IsAny<UrlShortener>()))
                .ThrowsAsync(new Exception("Url is invalid or unreacheable."));
            var controller = PrepareController(mockShortenerFacade);

            // Action
            var outcome = await controller.Post("http://facebook.com");

            // Assert
            Assert.Equal("Url is invalid or unreacheable.", outcome);
        }

        private static UrlController PrepareController(Mock<IShortenerFacade> mockShortenerFacade)
        {
            var controller = new UrlController(mockShortenerFacade.Object);
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
    }
}
