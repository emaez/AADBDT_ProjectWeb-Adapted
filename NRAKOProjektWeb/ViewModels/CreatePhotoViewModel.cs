using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.ViewModels
{
    public class CreatePhotoViewModel
    {
        [Required]
        [Display(Name ="Hashtags:")]
        public string HashtagsString { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile PhotoFile { get; set; }
        [Display(Name = "Resize")]
        public bool DoResize { get; set; }
        public int ResizeWidth { get; set; }
        public int ResizeHeight { get; set; }
        [Display(Name ="Convert")]
        public bool DoConversion { get; set; }
        public string ConversionExtension { get; set; }
        public string OriginalImageExtension { get; set; }
        public IEnumerable<string> AvailableFormats { get; set; }


    }
}
