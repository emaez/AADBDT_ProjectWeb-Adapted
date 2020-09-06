using NRAKOProjektWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.ViewModels
{
    public class ManageUserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool Enabled { get; set; }
        [Display(Name="Subscription model")]
        public string SubscriptionModelName { get; set; }
        public int SubscriptionModelId { get; set; }
    }
}
