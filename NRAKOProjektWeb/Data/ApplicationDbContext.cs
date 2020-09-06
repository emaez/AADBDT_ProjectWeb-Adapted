using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NRAKOProjektWeb.Data.Configurations;
using NRAKOProjektWeb.Models;

namespace NRAKOProjektWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<Models.NRAKOUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SubscriptionModel> SubscriptionModels { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<PhotoHashtag> PhotoHashtags { get; set; }
        public virtual DbSet<Hashtag> Hashtags { get; set; }
        public virtual DbSet<LogEntry> LogEntries { get; set; }
        public virtual DbSet<ConfigurationEntry> ConfigurationEntities { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PhotoHashtagConfiguration());
            builder.ApplyConfiguration(new ConfigurationEntryConfiguration());

            builder.Entity<IdentityRole>().HasData(new IdentityRole { ConcurrencyStamp = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { ConcurrencyStamp = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER" });

            builder.Entity<ConfigurationEntry>().HasData(new ConfigurationEntry { Name = "StorageType", Value = "Local" });
        }


    }
}
