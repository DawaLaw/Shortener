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
            var controller = new HomeController(mockShortenerFacade.Object);
            var shortener = new UrlShortener() { LongUrl = "http://facebook.com" };

            // Action
            var outcome = await controller.Index(shortener);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(outcome);
            Assert.Equal("https://localhost:44317/1234567", ((UrlShortener)viewResult.Model).ShortUrl);
        }
    }
}
