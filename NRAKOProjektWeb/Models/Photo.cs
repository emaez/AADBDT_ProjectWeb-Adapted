using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Models
{
    public class Photo
    {

        public Photo()
        {
            PhotosHashtags = new List<PhotoHashtag>();
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public long Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public virtual IList<PhotoHashtag> PhotosHashtags { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string NRAKOUserId { get; set; }

        public virtual NRAKOUser NRAKOUser { get; set; }
    }
}
