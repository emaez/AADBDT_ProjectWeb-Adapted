using NRAKOProjektWeb.Models;
using NRAKOProjektWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Interface.Repository
{
    public interface IUserRepository
    {
        NRAKOUser GetUser(ClaimsPrincipal claimPrincipal);
        NRAKOUser GetUserByID(string id);
        void UpdateUser(NRAKOUser user);
        bool IsInRole(NRAKOUser user, string role);
        SubscriptionModel GetSubscriptionModel(string userID);
        List<NRAKOUser> GetUsers();
        SubscriptionModel GetSubscriptionModelByID(int id);
        IEnumerable<SubscriptionModel> SubscriptionModels();
        List<LogEntryViewModel> GetLogUserEntries(string id);
    }
}
