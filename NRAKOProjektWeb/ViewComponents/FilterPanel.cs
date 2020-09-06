using Microsoft.AspNetCore.Mvc;
using NRAKOProjektWeb.Data;
using NRAKOProjektWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.ViewComponents
{
    public class FilterPanel : ViewComponent
    {

        private readonly ApplicationDbContext _db;


        public FilterPanel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke(FilterDataViewModel model)
        {

            model.MaxSize = (int)((_db.Photos.Max(p => p.Size) / 1024) + 1);
            model.MaxWidth = _db.Photos.Max(p => p.Width);
            model.MaxHeight = _db.Photos.Max(p => p.Height);

            return View("FilterPanel", model);
        }
    }
}
