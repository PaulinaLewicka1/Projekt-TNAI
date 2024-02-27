using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manga.Dto.Publishers
{
    //Obiekt wyjściowy 
    public class PublisherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public int MangaCount { get; set; }  
    }
}
