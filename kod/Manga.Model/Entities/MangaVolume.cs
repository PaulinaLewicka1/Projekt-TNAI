using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Manga.Model.Entities
{
    public class MangaVolume
    {
        public int Id { get; set; }
        public string? ISBN { get; set; }
        public string? Name { get; set; }
        public string? Authors { get; set; }
        public string? Description { get; set; } 
        public int? VolumeNumber { get; set; }
        public int? Year { get; set; }
        public int MangaSeriesId { get; set; }
        public virtual MangaSeries Series { get; set; }
    }
}
