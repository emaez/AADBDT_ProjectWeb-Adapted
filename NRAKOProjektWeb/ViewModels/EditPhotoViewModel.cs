using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.ViewModels
{
    public class EditPhotoViewModel
    {
        public int PhotoId { get; set; }
        public string Description { get; set; }
        public string Hashtags { get; set; }
        public string Url { get; set; }
    }
}
