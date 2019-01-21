using Microsoft.AspNetCore.Mvc;
using Shortener.Business;
using Shortener.Models;
using Shortener.Web.Controllers;
using System;
using Xunit;

namespace Shortener.Tests
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexPostTest_PassTest()
        {
            // Arrange
            var controller = new HomeController(new ShortenerFacade());
            var shortener = new UrlShortener() { LongUrl = "http://facebook.com" };

            // Action
            var outcome = controller.Index(shortener);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(outcome);
            Assert.Equal("https://localhost:53469/1234567", ((UrlShortener)viewResult.Model).ShortUrl);
        }
    }
}
