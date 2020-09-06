using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Models
{
    public class PhotoHashtag
    {
        public int PhotoId { get; set; }
        public Photo Photo { get; set; }
        public int HashtagId { get; set; }
        public Hashtag Hashtag { get; set; }
    }
}
