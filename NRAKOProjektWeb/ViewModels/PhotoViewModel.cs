using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.ViewModels
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public List<String> Hashtags { get; set; }
        public long Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public DateTime UploadedAt { get; set; }
        public string Author { get; set; }
        public bool Editable { get; set; }

    }
}
