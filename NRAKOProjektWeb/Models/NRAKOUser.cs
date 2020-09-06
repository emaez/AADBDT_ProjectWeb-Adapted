using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Models
{
    public class NRAKOUser :IdentityUser
    {
        public int SubscriptionModelId { get; set; }

        public virtual SubscriptionModel SubscriptionModel { get; set; }
        public virtual IEnumerable<Photo> Photos { get; set; }

        public bool Enabled { get; set; }
    }
}
