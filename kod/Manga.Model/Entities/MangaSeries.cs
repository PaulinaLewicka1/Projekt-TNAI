using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga.Model.Entities
{
    public class MangaSeries
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SeriesISBN { get; set; }
        public bool? Completed { get; set; }
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<MangaVolume> Volumes { get; set; }
    }
}
