using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NRAKOProjektWeb.Data;
using NRAKOProjektWeb.Interface.Repository;
using NRAKOProjektWeb.Models;
using NRAKOProjektWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<NRAKOUser> _userManager;
        private readonly ApplicationDbContext _db;
        public UserRepository(UserManager<NRAKOUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public NRAKOUser GetUser(ClaimsPrincipal claimPrincipal)
        {
            return _userManager.GetUserAsync(claimPrincipal).Result;
        }
        public NRAKOUser GetUserByID(string id)
        {
            return _userManager.FindByIdAsync(id).Result;
        }
        public void UpdateUser(NRAKOUser user)
        {
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public bool IsInRole(NRAKOUser user, string role)
        {
            return _userManager.IsInRoleAsync(user, role).Result;
        }
        public SubscriptionModel GetSubscriptionModel(string userID)
        {
            return _db.Users.Where(u => u.Id == userID).Include(u => u.SubscriptionModel).FirstOrDefault().SubscriptionModel;
        }
        public SubscriptionModel GetSubscriptionModelByID(int id)
        {
            return _db.SubscriptionModels.Find(id);
        }
        public List<NRAKOUser> GetUsers()
        {
            return _userManager.Users.ToList();
        }

        public IEnumerable<SubscriptionModel> SubscriptionModels()
        {
            return _db.SubscriptionModels;
        }
        public List<LogEntryViewModel> GetLogUserEntries(string id)
        {
            return _db.LogEntries
                .Where(le => le.UserId == id)
                .OrderByDescending(le => le.CretedAt)
                .Select(le => new LogEntryViewModel()
                {
                    CreatedAt = le.CretedAt,
                    Text = le.Text
                })
                .ToList();
        }
    }
}
