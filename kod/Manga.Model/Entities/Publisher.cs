using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga.Model.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Website { get; set; }
        public virtual ICollection<MangaSeries> MangaSeries { get; set; }
    }
}
