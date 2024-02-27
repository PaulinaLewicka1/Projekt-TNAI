using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manga.Model;
using Manga.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manga.Repository.MangaSerie
{
    public class MangaSeriesRepository : BaseRepository, IMangaSeriesRepository
    {
        public MangaSeriesRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<MangaSeries?> GetMangaSeriesByIdAsync(int id)
        {
            var mangaSeries = await DbContext.MangaSeries.Include(x => x.Volumes).SingleOrDefaultAsync(x => x.Id == id);
            return mangaSeries;
        }

        public async Task<List<MangaSeries>> GetAllMangaSeriesAsync()
        {
            var mangaSeries = await DbContext.MangaSeries.Include(x => x.Volumes).ToListAsync();
            return mangaSeries;
        }

        public async Task<bool> SaveMangaSeriesAsync(MangaSeries mangaSeries)
        {
            if (mangaSeries == null) return false;

            DbContext.Entry(mangaSeries).State = mangaSeries.Id == default(int) ? EntityState.Added : EntityState.Modified;

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex) { return false; }

            return true;
        }

        public async Task<bool> DeleteMangaSeriesAsync(int id)
        {
            var mangaSeries = await GetMangaSeriesByIdAsync(id);
            if(mangaSeries == null) { return true; }

            DbContext.MangaSeries.Remove(mangaSeries);

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex) { return false; }
            return true;
        }
    }
}
