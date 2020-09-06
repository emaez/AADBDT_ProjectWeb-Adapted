using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Models
{
    public class SubscriptionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxUploadSize { get; set; }
        public int DailyUploadLimit { get; set; }
        public int MaxNumberOfPhotos { get; set; }
        public double Price { get; set; }


        public virtual IEnumerable<NRAKOUser> NRAKOUsers { get; set; }
    }
}
