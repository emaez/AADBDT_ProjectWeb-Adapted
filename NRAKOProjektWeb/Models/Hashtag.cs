using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Models
{
    public class Hashtag
    {
        public Hashtag()
        {
            PhotosHashtags = new List<PhotoHashtag>();
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual IList<PhotoHashtag> PhotosHashtags { get; set; }
    }
}
