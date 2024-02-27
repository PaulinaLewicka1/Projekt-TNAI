using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manga.Model;
using Manga.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manga.Repository.MangaVolumes
{
    public class MangaVolumeRepository : BaseRepository, IMangaVolumeRepository
    {
        public MangaVolumeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<MangaVolume?> GetMangaVolumeByIdAsync(int id)
        {
            var mangaVolume = await DbContext.MangaVolumes.SingleOrDefaultAsync( x => x.Id == id);
            return mangaVolume;
        }
        public async Task<List<MangaVolume>> GetAllMangaVolumesAsync()
        {
            var mangaVolumes = await DbContext.MangaVolumes.ToListAsync();
            return mangaVolumes;
        }
        public async Task<bool> SaveMangaVolumeAsync(MangaVolume mangaVolume)
        {
            if(mangaVolume == null) return false;

            DbContext.Entry(mangaVolume).State = mangaVolume.Id == default(int) ? EntityState.Added : EntityState.Modified;
            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> DeleteMangaVolumeAsync(int id)
        {
            var mangaVolume = await GetMangaVolumeByIdAsync(id);

            if(mangaVolume == null) { return true; }

            DbContext.MangaVolumes.Remove(mangaVolume);

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch(Exception) { return false; }

            return true;
        }
    }
}
