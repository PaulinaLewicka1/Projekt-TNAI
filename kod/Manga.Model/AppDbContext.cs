using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manga.Model.Configurations;
using Manga.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace Manga.Model
{
    public class AppDbContext : IdentityDbContext
    { 
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<MangaSeries> MangaSeries { get; set; }
        public DbSet<MangaVolume> MangaVolumes { get; set; }

        public AppDbContext() : base() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());
            modelBuilder.ApplyConfiguration(new MangaVolumeConfiguration());
            modelBuilder.ApplyConfiguration(new MangaSeriesConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
