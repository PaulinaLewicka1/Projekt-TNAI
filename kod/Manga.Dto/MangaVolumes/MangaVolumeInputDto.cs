using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga.Dto.MangaVolumes
{
    public class MangaVolumeInputDto
    {
        [Required]
        [MaxLength(20)]
        public string ISBN { get; set; }
          
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Authors { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        public int VolumeNumber { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int MangaSeriesId { get; set; }
    }
}
