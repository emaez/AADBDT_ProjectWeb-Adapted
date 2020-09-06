using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NRAKOProjektWeb.Interface.Repository;
using NRAKOProjektWeb.Models;
using NRAKOProjektWeb.Patterns.ChainOfResponsibility;
using NRAKOProjektWeb.Patterns.Facade;
using NRAKOProjektWeb.Patterns.MutationFactory;
using NRAKOProjektWeb.Patterns.Singleton;
using NRAKOProjektWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NRAKOProjektWeb.Controllers
{
    [Authorize]
    public class PhotosController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly INRAKOLogger _logger;
        private readonly AmazonS3Tools _amazonS3Tools;
        private readonly IMutationActionFactorySelector _mutationActionFactorySelector;


        public PhotosController(
            IUserRepository userRepository,
            IPhotoRepository photoRepository,
            INRAKOLogger logger,
            AmazonS3Tools amazonS3Tools,
            IMutationActionFactorySelector mutationActionFactorySelector)
        {
            _photoRepository = photoRepository;
            _userRepository = userRepository;
            _logger = logger;
            _amazonS3Tools = amazonS3Tools;
            _mutationActionFactorySelector = mutationActionFactorySelector;
        }

        private NRAKOUser CurrentUser
        {
            get
            {
                return _userRepository.GetUser(HttpContext?.User);
            }
        }
        private bool IsAdmin
        {
            get
            {
                return CurrentUser == null ? false : _userRepository.IsInRole(CurrentUser, "Admin");
            }
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.ShowFilter = true;
            List<PhotoViewModel> model = new List<PhotoViewModel>();

            string userId = CurrentUser == null ? null : CurrentUser.Id;

            var photos = _photoRepository.GetPhotos();

            model = Helpers.Helpers.GetPhotoViewModelsFromPhotos(photos, userId, IsAdmin).ToList();

            IEnumerable<Photo> photosEnumerable = _photoRepository.PhotoDefaultIfEmpty();

            Helpers.Helpers.SetFilterBounds(photosEnumerable, ViewBag);

            return View(model);
        }

        public IActionResult CreateNewPhoto()
        {
            var userPhotos = _photoRepository.GetphotosByUserID(CurrentUser.Id);

            MaxNumberOfPhotosExceededSubscriptionValidationChecker checker = new MaxNumberOfPhotosExceededSubscriptionValidationChecker();
            checker.SetNext(new DailyUploadLimitExceededSubscriptionValidationChecker());

            SubscriptionModel sm = _userRepository.GetSubscriptionModel(CurrentUser.Id);
            SubscriptionModel uc = new SubscriptionModel()
            {
                DailyUploadLimit = userPhotos.Where(up => up.DateCreated.Date == DateTime.Today).Count(),
                MaxNumberOfPhotos = userPhotos.Count()
            };

            string violation = checker.CheckViolation(sm, uc);

            if (violation != null)
            {
                ViewBag.Violation = violation;
                return View("Violation");
            }

            CreatePhotoViewModel cpvm = new CreatePhotoViewModel();
            cpvm.AvailableFormats = Helpers.Helpers.GetAvailableFormats();

            return View(cpvm);
        }

        [HttpPost]
        public IActionResult CreateNewPhoto(CreatePhotoViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            Photo photo = _photoRepository.SavePhoto(model, CurrentUser?.Id);

            _logger.Log(CurrentUser?.Id, $"Created a new photo @{photo.Url}");


            return RedirectToAction("Index");
        }

        public IActionResult UserPhotos(string id)
        {

            if (id == null)
            {
                id = CurrentUser.Id;
            }

            var userPhotos = _photoRepository.GetphotosByUserID(id);

            var userPhotosViewModels = Helpers.Helpers.GetPhotoViewModelsFromPhotos(userPhotos, CurrentUser?.Id, IsAdmin);

            return View("Index", userPhotosViewModels);
        }

        public IActionResult EditPhoto(int id)
        {


            Photo photo = _photoRepository.GetphotoByID(id);

            if (photo == null)
            {
                return RedirectToAction("Index");
            }

            if (photo.NRAKOUserId != CurrentUser.Id
                && !(IsAdmin))
            {
                return RedirectToAction("Index");
            }


            EditPhotoViewModel epvm = Helpers.Helpers.GetEditPhotoViewModelFromPhoto(photo);

            return View(epvm);
        }
        [HttpPost]
        public IActionResult EditPhoto(EditPhotoViewModel epvm)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            Photo photo = _photoRepository.GetphotoByID(epvm.PhotoId);
            if (photo == null)
            {
                return RedirectToAction("Index");
            }

            if (photo.NRAKOUserId != CurrentUser.Id
                && !(IsAdmin))
            {
                return RedirectToAction("Index");
            }

            photo.DateModified = DateTime.Now;
            photo.Description = epvm.Description;

            string[] hashtags = epvm.Hashtags.Split(' ');

            _photoRepository.RemovePhotoHashTags(photo.Id);

            _photoRepository.AddHashtagsToPhoto(hashtags, photo.Id);

            _logger.Log(CurrentUser.Id, $"Edited photo @{photo.Url}, Description: {photo.Description}, Hashtags: {epvm.Hashtags}");

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public IActionResult FilterPhotos(string sizeRange, string widthRange, string heightRange, string hashtagsFilter, string authorFilter)
        {
            ViewBag.ShowFilter = true;


            string userId = CurrentUser == null ? null : CurrentUser.Id;


            IEnumerable<Photo> photos = _photoRepository.FilterData(sizeRange, widthRange, heightRange, hashtagsFilter, authorFilter, ViewBag);

            List<PhotoViewModel> model = Helpers.Helpers.GetPhotoViewModelsFromPhotos(photos
                .ToList(), userId, IsAdmin
                )
                .ToList();

            return View("Index", model);
        }

        public IActionResult DownloadPhoto(int id)
        {
            Photo photo = _photoRepository.GetphotoByID(id);
            if (photo == null)
            {
                return RedirectToAction("Index");
            }

            DownloadPhotoViewModel dpvm = Helpers.Helpers.GetDownloadPhotoViewModelFromPhoto(photo);
            return View(dpvm);
        }

        [HttpPost]
        public IActionResult DownloadPhoto(DownloadPhotoViewModel model)
        {

            IEnumerable<MutationAction> actions = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<MutationAction>>(model.ActionList);

            var extension = model.OriginalImageExtension;
            if (model.DoConversion)
            {
                extension = model.ConversionExtension;
            }

            byte[] fileBytes = Helpers.Helpers.PerformMutations(model.PhotoUrl, extension, actions, Request, _mutationActionFactorySelector);

            _logger.Log(CurrentUser.Id, $"Downloaded photo with actions: ${model.ActionList} in format: ${extension}");

            return File(fileBytes, "application/force-download", Path.GetFileNameWithoutExtension(model.PhotoUrl) + "." + extension);
        }

    }
}