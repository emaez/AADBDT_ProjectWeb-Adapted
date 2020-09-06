using NRAKOProjektWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.ViewModels
{
    public class UserStatisticsAndActionsViewModel
    {

        public IEnumerable<LogEntryViewModel> LogEntries { get; set; }
        public string Username { get; set; }
        public SubscriptionModel SubscriptionModel { get; set; }
        public int DataConsumption { get; set; }
        public int PictureCount { get; set; }
        public int PhotoCountPercentage { get; set; }
        public int TodayUploaded { get; set; }
        public int TodayUploadedPercentage { get; set; }
    }
}
