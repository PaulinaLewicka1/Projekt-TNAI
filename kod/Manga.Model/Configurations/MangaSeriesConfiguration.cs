using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manga.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manga.Model.Configurations
{
    public class MangaSeriesConfiguration : IEntityTypeConfiguration<MangaSeries>
    {
        public void Configure(EntityTypeBuilder<MangaSeries> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.SeriesISBN).HasMaxLength(20);
            builder.Property(x => x.Name).HasMaxLength(200);
            builder.HasOne(x => x.Publisher).WithMany(x => x.MangaSeries).HasForeignKey(x => x.PublisherId);
        }
    }
}
