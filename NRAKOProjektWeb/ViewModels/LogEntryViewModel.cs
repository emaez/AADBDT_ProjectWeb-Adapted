using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.ViewModels
{
    public class LogEntryViewModel
    {
        [Display(Name ="Action time:")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Action:")]
        public string Text { get; set; }

    }
}
