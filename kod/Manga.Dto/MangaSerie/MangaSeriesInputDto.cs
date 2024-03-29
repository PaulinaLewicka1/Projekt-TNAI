﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga.Dto.MangaSerie
{
    public class MangaSeriesInputDto
    {
        [Required]
        [MaxLength(20)]
        public string SeriesISBN { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public bool Completed { get; set; }

        [Required]
        public int PublisherId { get; set; }

    }
}
