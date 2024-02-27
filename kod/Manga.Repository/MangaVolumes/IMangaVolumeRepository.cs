using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manga.Model.Entities;

namespace Manga.Repository.MangaVolumes
{
    public interface IMangaVolumeRepository
    {
        Task<MangaVolume?> GetMangaVolumeByIdAsync(int id);
        Task<List<MangaVolume>> GetAllMangaVolumesAsync();
        Task<bool> SaveMangaVolumeAsync(MangaVolume mangaVolume);
        Task<bool> DeleteMangaVolumeAsync(int id);
    }
}
