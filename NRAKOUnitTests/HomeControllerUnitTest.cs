using Microsoft.AspNetCore.Mvc;
using NRAKOProjektWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NRAKOUnitTests
{
    public class HomeControllerUnitTest
    {
        [Fact]
        public void Index()
        {
            //arrange
            var controller = new HomeController();

            //act
            var result = controller.Index();

            // assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Photos", redirectToActionResult.ControllerName);
        }
        [Fact]
        public void Privacy()
        {
            //arrange
            var controller = new HomeController();

            //act
            var result = controller.Privacy();

            // assert
            Assert.IsAssignableFrom<ViewResult>(result);
        }
        //[Fact]
        //public void Error()
        //{
        //    //arrange
        //    var controller = new HomeController();

        //    //act
        //    var result = controller.Error();

        //    // assert
        //    Assert.IsAssignableFrom<ViewResult>(result);
        //}
    }
}
