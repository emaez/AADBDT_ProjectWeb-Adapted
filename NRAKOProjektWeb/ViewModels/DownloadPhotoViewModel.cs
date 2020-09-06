using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.ViewModels
{
    public class DownloadPhotoViewModel
    {
        public string ActionList { get; set; }
        public string PhotoUrl { get; set; }
        public IEnumerable<string> AvailableActions { get; set; }
        public IEnumerable<string> AvailableFormats { get; set; }
        public int ResizeWidth { get; set; }
        public int ResizeHeight { get; set; }
        public string OriginalImageExtension { get; set; }
        public bool DoConversion { get; set; }
        public string ConversionExtension { get; set; }
    }
}
