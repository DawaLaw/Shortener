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
            var shortener = new UrlShortener()
            {
                LongUrl = "http://facebook.com",
                UrlId = "1234567"
            };

            // Action
            var outcome = await controller.Index(shortener);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(outcome);
            Assert.Equal("https://localhost:44317/1234567", ((UrlShortener)viewResult.Model).ShortUrl);
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
    }
}
