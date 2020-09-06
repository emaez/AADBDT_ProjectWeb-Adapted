using Microsoft.AspNetCore.Mvc;
using Moq;
using NRAKOProjektWeb.Controllers;
using NRAKOProjektWeb.Interface.Repository;
using NRAKOProjektWeb.Models;
using NRAKOProjektWeb.Patterns.Facade;
using NRAKOProjektWeb.Patterns.MutationFactory;
using NRAKOProjektWeb.Patterns.Singleton;
using NRAKOProjektWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NRAKOUnitTests
{
    public class PhotoControllerUnitTests
    {
        [Fact]
        public void Index()
        {
            // arrange
            var mockUserRep = new Mock<IUserRepository>();
            var mockPhotoRep = new Mock<IPhotoRepository>();
            var mockLogger = new Mock<INRAKOLogger>();
            var mockS3Amazon = new Mock<AmazonS3Tools>();
            var mockMutation = new Mock<IMutationActionFactorySelector>();

            mockUserRep.Setup(x => x.GetUser(null)).Returns((NRAKOUser)null);
            mockPhotoRep.Setup(x => x.GetPhotos()).Returns(GetPhotos());
            mockPhotoRep.Setup(x => x.PhotoDefaultIfEmpty()).Returns(new List<Photo>() {
                new Photo { Size = 0, Width = 0, Height = 0 }
            });

            var controller = new PhotosController(mockUserRep.Object, mockPhotoRep.Object, mockLogger.Object, mockS3Amazon.Object, mockMutation.Object);
     
            // act
            var result = controller.Index();
            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<PhotoViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void CreateNewPhoto()
        {
            // arrange
            var mockUserRep = new Mock<IUserRepository>();
            var mockPhotoRep = new Mock<IPhotoRepository>();
            var mockLogger = new Mock<INRAKOLogger>();
            var mockS3Amazon = new Mock<AmazonS3Tools>();
            var mockMutation = new Mock<IMutationActionFactorySelector>();
            CreatePhotoViewModel photoVM = new CreatePhotoViewModel()
            {
                Description = "Test",
                DoConversion = false,
                DoResize = false,
                HashtagsString = "Test",
                OriginalImageExtension = "png"
            };

            mockUserRep.Setup(x => x.GetUser(null)).Returns((NRAKOUser)null);
            mockLogger.Setup(x => x.Log(null, null));
            mockPhotoRep.Setup(x => x.SavePhoto(photoVM, null)).Returns(new Photo { Size = 0, Width = 0, Height = 0 });

            var controller = new PhotosController(mockUserRep.Object, mockPhotoRep.Object, mockLogger.Object, mockS3Amazon.Object, mockMutation.Object);

            // act
            var result = controller.CreateNewPhoto(photoVM);

            // assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Null( redirectToActionResult.ControllerName);
        }

        [Fact]
        public void UserPhotos()
        {
            // arrange
            var mockUserRep = new Mock<IUserRepository>();
            var mockPhotoRep = new Mock<IPhotoRepository>();
            var mockLogger = new Mock<INRAKOLogger>();
            var mockS3Amazon = new Mock<AmazonS3Tools>();
            var mockMutation = new Mock<IMutationActionFactorySelector>();

            var id = "testID";
            mockUserRep.Setup(x => x.GetUser(null)).Returns((NRAKOUser)null);
            mockPhotoRep.Setup(x => x.GetphotosByUserID(id)).Returns(GetPhotos());

            var controller = new PhotosController(mockUserRep.Object, mockPhotoRep.Object, mockLogger.Object, mockS3Amazon.Object, mockMutation.Object);

            // act
            var result = controller.UserPhotos(id);

            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<PhotoViewModel>>(
                 viewResult.ViewData.Model);
            Assert.Equal(2, model.Count);
        }
        #region private
        private List<Photo> GetPhotos()
        {
            return new List<Photo>()
            {
                new Photo()
                {
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Description = "Test description",
                    Height = 500,
                    Id = 1,
                    Size = 500,
                    Width = 500
                },
                new Photo()
                {
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Description = "Test description",
                    Height = 500,
                    Id = 1,
                    Size = 500,
                    Width = 500
                }
            };
        }
        #endregion
    }
}
