using NRAKOProjektWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.Singleton
{
    public class NRAKOLogger:INRAKOLogger
    {
        private readonly ApplicationDbContext _db;

        public NRAKOLogger(ApplicationDbContext db)
        {
            _db = db;
        }



        public void Log(string userId, string text)
        {
            _db.LogEntries.Add(new Models.LogEntry()
            {
                CretedAt = DateTime.Now,
                Text = text,
                UserId = userId
            });

            _db.SaveChanges();
        }
    }
}
