using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manga.Model.Entities;

namespace Manga.Repository.MangaSerie
{
    public interface IMangaSeriesRepository
    {
        Task<MangaSeries?> GetMangaSeriesByIdAsync(int id);
        Task<List<MangaSeries>> GetAllMangaSeriesAsync();
        Task<bool> SaveMangaSeriesAsync(MangaSeries mangaSeries);
        Task<bool> DeleteMangaSeriesAsync(int id);
    }
}
