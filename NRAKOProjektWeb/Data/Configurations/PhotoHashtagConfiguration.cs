using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NRAKOProjektWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Data.Configurations
{
    public class PhotoHashtagConfiguration : IEntityTypeConfiguration<PhotoHashtag>
    {
        public void Configure(EntityTypeBuilder<PhotoHashtag> builder)
        {
            builder.HasKey(ph => new { ph.PhotoId, ph.HashtagId });

            builder.HasOne<Photo>(ph => ph.Photo)
                .WithMany(p => p.PhotosHashtags)
                .HasForeignKey(ph => ph.PhotoId);

            builder.HasOne<Hashtag>(ph => ph.Hashtag)
                .WithMany(h => h.PhotosHashtags)
                .HasForeignKey(ph => ph.HashtagId);
        }
    }
}
