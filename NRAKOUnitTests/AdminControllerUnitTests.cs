using Microsoft.AspNetCore.Mvc;
using Moq;
using NRAKOProjektWeb.Controllers;
using NRAKOProjektWeb.Interface.Repository;
using NRAKOProjektWeb.Models;
using NRAKOProjektWeb.Patterns.Singleton;
using NRAKOProjektWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NRAKOUnitTests
{
    public class AdminControllerUnitTests
    {
        [Fact]
        public void ManageUsers()
        {
            // arrange
            var mockUserRep = new Mock<IUserRepository>();
            var mockPhotoRep = new Mock<IPhotoRepository>();
            var mockLogger = new Mock<INRAKOLogger>();

            mockUserRep.Setup(x => x.GetUser(null)).Returns((NRAKOUser)null);
            mockUserRep.Setup(x => x.GetUsers()).Returns(GetUsers());
            mockUserRep.Setup(x => x.GetSubscriptionModelByID(1)).Returns(new SubscriptionModel()
            {
                DailyUploadLimit = 1000,
                Id = 1,
                MaxNumberOfPhotos = 10,
                MaxUploadSize = 1024,
                Name = "Subscription Model 1"
            });

            var controller = new AdminController(mockUserRep.Object, mockPhotoRep.Object, mockLogger.Object);

            // act
            var result = controller.ManageUsers();

            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ManageUserViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
        [Fact]
        public void EditUser()
        {
            // arrange
            var mockUserRep = new Mock<IUserRepository>();
            var mockPhotoRep = new Mock<IPhotoRepository>();
            var mockLogger = new Mock<INRAKOLogger>();

            var id = "TestID1";

            mockUserRep.Setup(x => x.GetUserByID(id)).Returns(GetUser());

            mockUserRep.Setup(x => x.SubscriptionModels()).Returns(GetSubscriptionModels());
            mockUserRep.Setup(x => x.GetSubscriptionModelByID(1)).Returns(GetSubscription());

            var controller = new AdminController(mockUserRep.Object, mockPhotoRep.Object, mockLogger.Object);

            // act
            var result = controller.EditUser(id);

            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ManageUserViewModel>(
                viewResult.ViewData.Model);
            Assert.NotNull(model);
        }
        [Fact]
        public void UserStatiscticsAndActions()
        {
            // arrange
            var mockUserRep = new Mock<IUserRepository>();
            var mockPhotoRep = new Mock<IPhotoRepository>();
            var mockLogger = new Mock<INRAKOLogger>();

            var id = "TestID1";

            mockUserRep.Setup(x => x.GetUserByID(id)).Returns(GetUser());
            mockUserRep.Setup(x => x.GetLogUserEntries(id)).Returns(GetLogEntries());
            mockUserRep.Setup(x => x.GetSubscriptionModelByID(1)).Returns(GetSubscription());
            mockPhotoRep.Setup(x => x.GetphotosByUserID(id)).Returns(GetPhotos());


            var controller = new AdminController(mockUserRep.Object, mockPhotoRep.Object, mockLogger.Object);

            // act
            var result = controller.UserStatiscticsAndActions(id);

            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<UserStatisticsAndActionsViewModel>(
                viewResult.ViewData.Model);
            Assert.NotNull(model);
        }
        #region private
        private SubscriptionModel GetSubscription()
        {
            return new SubscriptionModel()
            {
                DailyUploadLimit = 1000,
                Id = 1,
                MaxNumberOfPhotos = 10,
                MaxUploadSize = 1024,
                Name = "Subscription Model 1"
            };
        }
        private NRAKOUser GetUser()
        {
            return new NRAKOUser()
            {
                AccessFailedCount = 0,
                Email = "test@gmail.com",
                Enabled = true,
                Id = "TestID1",
                NormalizedEmail = "TEST@GMAIL.COM",
                NormalizedUserName = "TEST",
                UserName = "test",
                PhoneNumber = "009287638367",
                SubscriptionModelId = 1
            };
        }
        private List<NRAKOUser> GetUsers()
        {
            return new List<NRAKOUser>(){
                new NRAKOUser()
                {
                    AccessFailedCount = 0,
                    Email = "test@gmail.com",
                    Enabled = true,
                    Id = "TestID1",
                    NormalizedEmail = "TEST@GMAIL.COM",
                    NormalizedUserName = "TEST",
                    UserName = "test",
                    PhoneNumber = "009287638367",
                    SubscriptionModelId = 1
                },
                new NRAKOUser()
                {
                    AccessFailedCount = 0,
                    Email = "test1@gmail.com",
                    Enabled = true,
                    Id = "TestID2",
                    NormalizedEmail = "TEST1@GMAIL.COM",
                    NormalizedUserName = "TEST1",
                    UserName = "test1",
                    PhoneNumber = "00928555367",
                    SubscriptionModelId = 2
                }
            };
        }
        private List<SubscriptionModel> GetSubscriptionModels()
        {
            return new List<SubscriptionModel>(){
                new SubscriptionModel()
                {
                    Id = 1,
                    Name = "Subscription Model 1"
                },
                new SubscriptionModel()
                {
                     Id = 2,
                    Name = "Subscription Model 1"
                }
            };
        }
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

        private List<LogEntryViewModel> GetLogEntries()
        {
            return new List<LogEntryViewModel>()
            {
                new LogEntryViewModel()
                {
                    CreatedAt = DateTime.Now,
                    Text = "log entry 1"
                },
                new LogEntryViewModel()
                {
                    CreatedAt = DateTime.Now.AddDays(-1),
                    Text = "log entry 2"
                },
                new LogEntryViewModel()
                {
                    CreatedAt = DateTime.Now.AddDays(-2),
                    Text = "log entry 3"
                },
                new LogEntryViewModel()
                {
                    CreatedAt = DateTime.Now.AddDays(-3),
                    Text = "log entry 4"
                }
            };
        }
        #endregion
    }
}
