using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NRAKOProjektWeb.Data;
using NRAKOProjektWeb.Models;
using NRAKOProjektWeb.Patterns.Singleton;
using System;
using System.Linq;

namespace NRAKOProjektWeb.Areas.Identity.Pages.Account.Manage
{
    public class ManagePackageModel : PageModel
    {
        private readonly SignInManager<Models.NRAKOUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _db;
        private readonly INRAKOLogger _logger;


        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            public NRAKOUser User { get; set; }
            public SubscriptionModel SubscriptionModel { get; set; }
            public int DataConsumption { get; set; }
            public int PictureCount { get; set; }
            public int PhotoCountPercentage { get; set; }
            public int TodayUploaded { get; set; }
            public int TodayUploadedPercentage { get; set; }
            public int SubscriptionModelId { get; set; }

        }
        public ManagePackageModel(SignInManager<Models.NRAKOUser> signInManager,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext db,
            INRAKOLogger logger
        )
        {
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _db = db;
            _logger = logger;


        }
        public void OnGet()
        {
            Input = new InputModel();

            Input.User = _signInManager.UserManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
            Input.SubscriptionModel = _db.Users
                .Include(u => u.SubscriptionModel)
                .Where(u => u.Id == Input.User.Id)
                .FirstOrDefault()
                .SubscriptionModel;

            var userPhotos = _db.Photos.Where(p => p.NRAKOUserId == Input.User.Id);
            Input.PictureCount = userPhotos.Count();
            Input.TodayUploaded = userPhotos.Where(up => up.DateCreated.Date == DateTime.Today).Count();
            Input.DataConsumption = (int)Math.Round((decimal)userPhotos.Sum(up => up.Size)/1024/1024, MidpointRounding.AwayFromZero);

            Input.PhotoCountPercentage = (Input.PictureCount *100 / Input.SubscriptionModel.MaxNumberOfPhotos);
            Input.TodayUploadedPercentage = Input.TodayUploaded * 100 / Input.SubscriptionModel.DailyUploadLimit;
            Input.SubscriptionModelId = Input.SubscriptionModel.Id;
        }
        
        public void OnPost() {
            _db.Users.Find(_signInManager.UserManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result.Id).SubscriptionModelId = Input.SubscriptionModelId;
            _db.SaveChanges();

            Input.User = _signInManager.UserManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
            Input.SubscriptionModel = _db.Users
                .Include(u => u.SubscriptionModel)
                .Where(u => u.Id == Input.User.Id)
                .FirstOrDefault()
                .SubscriptionModel;

            var userPhotos = _db.Photos.Where(p => p.NRAKOUserId == Input.User.Id);
            Input.PictureCount = userPhotos.Count();
            Input.TodayUploaded = userPhotos.Where(up => up.DateCreated.Date == DateTime.Today).Count();
            Input.DataConsumption = (int)Math.Round((decimal)userPhotos.Sum(up => up.Size) / 1024 / 1024, MidpointRounding.AwayFromZero);

            Input.PhotoCountPercentage = (Input.PictureCount * 100 / Input.SubscriptionModel.MaxNumberOfPhotos);
            Input.TodayUploadedPercentage = Input.TodayUploaded * 100 / Input.SubscriptionModel.DailyUploadLimit;
            Input.SubscriptionModelId = Input.SubscriptionModel.Id;

            _logger.Log(Input.User.Id, $"User changed his subscription model to {Input.SubscriptionModel.Name}");

        }
    }
}