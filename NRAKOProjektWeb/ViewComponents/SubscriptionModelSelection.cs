using Microsoft.AspNetCore.Mvc;
using NRAKOProjektWeb.Data;
using NRAKOProjektWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.ViewComponents
{
    public class SubscriptionModelSelection:ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public SubscriptionModelSelection(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke() {

            IEnumerable<SubscriptionModelsViewModel> model = new List<SubscriptionModelsViewModel>();

            model = _db.SubscriptionModels.Select(sm => new SubscriptionModelsViewModel {
                DailyUploadLimit = sm.DailyUploadLimit,
                Id = sm.Id,
                MaxNumberOfPhotos = sm.MaxNumberOfPhotos,
                MaxUploadSize = sm.MaxUploadSize,
                Name = sm.Name,
                Price = sm.Price
            });


            return View("SubscriptionModelSelection", model);
        }
    }
}
