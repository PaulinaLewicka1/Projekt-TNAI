using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga.Dto.MangaVolumes
{
    public class MangaVolumeDto
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }
        public int VolumeNumber { get; set; }
        public int Year { get; set; }
        public int MangaSeriesId { get; set; }
    }
}
