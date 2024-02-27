using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Manga.Model.Entities;
namespace Manga.Model.Configurations
{
    public class MangaVolumeConfiguration : IEntityTypeConfiguration<MangaVolume>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MangaVolume> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ISBN).HasMaxLength(20);
            builder.Property(x => x.Name).HasMaxLength(200);
            builder.Property(x => x.Authors).HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(2000);
            builder.HasOne(x => x.Series).WithMany(x => x.Volumes).HasForeignKey(x => x.MangaSeriesId);
        }
    }
}
