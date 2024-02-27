using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manga.Model;
using Manga.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manga.Repository.Publishers
{
    public class PublisherRepository : BaseRepository, IPublisherRepository
    {
        public PublisherRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<Publisher?> GetPublisherByIdAsync(int id)
        {
            var publisher = await DbContext.Publishers.Include(x => x.MangaSeries)
                            .SingleOrDefaultAsync(x => x.Id == id);
            return publisher;
        }
        public async Task<List<Publisher>> GetAllPublishersAsync()
        {
            var publishers = await DbContext.Publishers.Include(x => x.MangaSeries).ToListAsync();
            return publishers;
        }
        public async Task<bool> SavePublisherAsync(Publisher publisher)
        {
            if (publisher == null) { return false; }

            DbContext.Entry(publisher).State = publisher.Id == default(int) ? EntityState.Added : EntityState.Modified;

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (Exception ex) { return false; }

            return true;
        }
        public async Task<bool> DeletePublisherAsync(int id)
        {
            var publisher = await GetPublisherByIdAsync(id);

            if(publisher == null) { return true; }

            DbContext.Publishers.Remove(publisher);

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch(Exception) { return false; }

            return true;
        }
    }
}
