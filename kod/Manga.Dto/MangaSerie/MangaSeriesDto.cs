using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga.Dto.MangaSerie
{
    public class MangaSeriesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeriesISBN { get; set; }
        public bool Completed { get; set; }
        public int VolumeCount { get; set; }
        public int PublisherId { get; set; }
    }
}
