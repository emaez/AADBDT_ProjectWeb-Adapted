using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRAKOProjektWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Data.Configurations
{
    public class ConfigurationEntryConfiguration : IEntityTypeConfiguration<ConfigurationEntry>
    {
        public void Configure(EntityTypeBuilder<ConfigurationEntry> builder)
        {
            builder.HasKey(ce=> ce.Name);
        }
    }
}
