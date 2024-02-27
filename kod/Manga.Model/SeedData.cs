using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manga.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Manga.Model
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            if (context.Publishers.Any())
            {
                return;
            }

            context.Publishers.AddRange(
                new Publisher()
                {
                    Name = "Waneko",
                    Website = "https://waneko.pl/"
                },
                new Publisher()
                {
                    Name = "J.P.Fantastica",
                    Website = "https://www.jpf.com.pl/"
                }
                );

            context.SaveChanges();

            if (context.MangaSeries.Any()) { return; }

            context.MangaSeries.AddRange(
                new MangaSeries()
                {
                    Name = "Naruto",
                    SeriesISBN = "978-83-8950-522-4",
                    Completed = true,
                    PublisherId = context.Publishers.Where(x => x.Name == "J.P.Fantastica").First().Id
                },
                new MangaSeries()
                {
                    Name = "Jujutsu Kaisen",
                    SeriesISBN = "978-83-8096-465-5",
                    Completed = false,
                    PublisherId = context.Publishers.Where(x => x.Name == "Waneko").First().Id
                }
                );

            context.SaveChanges();

            if (context.MangaVolumes.Any()) { return; }

            context.MangaVolumes.AddRange(
                new MangaVolume()
                {
                    ISBN = "978-83-8950-523-1",
                    Name = "Naruto",
                    Authors = "Masashi Kishimoto",
                    Description = "Osada Konoha-gakure. Naruto, uczeń szkoły dla ninja, rozrabia jak tylko potrafi! " +
                    "Mimo to, jego marzeniem jest pokonanie \"Hokage\", czyli aktualnego przywódcy wojowników. Gdy" +
                    " Naruto się rodził, wydarzyło się coś, o czym on sam wciąż jeszcze nie wie...",
                    VolumeNumber = 1,
                    Year = 1999,
                    MangaSeriesId = context.MangaSeries.Where(x => x.Name == "Naruto").First().Id
                },
                new MangaVolume()
                {
                    ISBN = "978-83-8096-466-2",
                    Name = "Jujutsu Kaisen",
                    Authors = "Gege Akutami",
                    Description = "Yuuji Itadori, licealista o niespotykanej tężyźnie fizycznej, każdego dnia odwiedza w " +
                    "szpitalu chorego dziadka. Zwykła codzienność zmienia się w chwili, kiedy jego przyjaciółka " +
                    "zdejmuje pieczęcie z przeklętego przedmiotu spoczywającego na terenie szkoły, przywołując tym samym " +
                    "niebezpieczne istoty.",
                    VolumeNumber = 1,
                    Year = 2018,
                    MangaSeriesId = context.MangaSeries.Where(x => x.Name == "Jujutsu Kaisen").First().Id
                }
                );
            context.SaveChanges();
        }
    }
}
