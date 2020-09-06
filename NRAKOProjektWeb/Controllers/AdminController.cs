using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NRAKOProjektWeb.Data;
using NRAKOProjektWeb.Interface.Repository;
using NRAKOProjektWeb.Models;
using NRAKOProjektWeb.Patterns.Singleton;
using NRAKOProjektWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NRAKOProjektWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly INRAKOLogger _logger;


        public AdminController(IUserRepository userRepository, IPhotoRepository photoRepository,
            INRAKOLogger logger)
        {
            _photoRepository = photoRepository;
            _userRepository = userRepository;
            _logger = logger;
        }
        private NRAKOUser CurrentUser
        {
            get
            {
                return _userRepository.GetUser(HttpContext?.User);
            }
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageUsers()
        {

            var users = _userRepository.GetUsers();

            IEnumerable<ManageUserViewModel> model = users.Select(u => new ManageUserViewModel()
            {
                Id = u.Id,
                Email = u.Email,
                Username = u.UserName,
                Enabled = u.Enabled,
                SubscriptionModelName = _userRepository.GetSubscriptionModelByID(u.SubscriptionModelId)?.Name,
                SubscriptionModelId = u.SubscriptionModelId
            });

            return View(model);
        }

        public IActionResult EditUser(string id)
        {

            var user = _userRepository.GetUserByID(id);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.SubscriptionModels = _userRepository.SubscriptionModels()
                .Select(sm => new SelectListItem { Text = sm.Name, Value = sm.Id.ToString() }).ToList();

            var model = new ManageUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Enabled = user.Enabled,
                SubscriptionModelName = _userRepository.GetSubscriptionModelByID(user.SubscriptionModelId)?.Name,
                SubscriptionModelId = user.SubscriptionModelId
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditUser(ManageUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _userRepository.GetUserByID(model.Id);

            if (user == null)
            {
                return RedirectToAction("ManageUsers");
            }

            user.Email = model.Email;
            user.Enabled = model.Enabled;
            user.SubscriptionModelId = model.SubscriptionModelId;

            _userRepository.UpdateUser(user);

            _logger.Log(CurrentUser.Id, $"Edited user: {user.UserName}; Set Email: {model.Email}, Enabled: {model.Enabled}, SubscriptionModelId: {model.SubscriptionModelId}");

            return RedirectToAction("ManageUsers");
        }

        public IActionResult UserStatiscticsAndActions(string id)
        {

            var user = _userRepository.GetUserByID(id);

            if (user == null)
            {
                return RedirectToAction("ManageUsers");
            }

            var userPhotos = _photoRepository.GetphotosByUserID(user.Id);

            UserStatisticsAndActionsViewModel usaavm = new UserStatisticsAndActionsViewModel();
            usaavm.SubscriptionModel = _userRepository.GetSubscriptionModelByID(user.SubscriptionModelId);
            usaavm.DataConsumption = (int)Math.Round((decimal)userPhotos.Sum(up => up.Size) / 1024 / 1024, MidpointRounding.AwayFromZero);
            usaavm.PictureCount = userPhotos.Count();
            usaavm.TodayUploaded = userPhotos.Where(up => up.DateCreated.Date == DateTime.Today).Count();

            usaavm.PhotoCountPercentage = (usaavm.PictureCount * 100 / usaavm.SubscriptionModel.MaxNumberOfPhotos);
            usaavm.TodayUploadedPercentage = usaavm.TodayUploaded * 100 / usaavm.SubscriptionModel.DailyUploadLimit;

            usaavm.LogEntries = _userRepository.GetLogUserEntries(user.Id);

            return View(usaavm);
        }


    }
}