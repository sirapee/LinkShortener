using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Data;
using LinkShortener.Entities;
using LinkShortener.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.RepositoryImplemtation
{
    public class LinkRepository : ILinkRepository
    {
   
        private readonly DataContext _dataContext;
        public LinkRepository(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        
        public async Task<IEnumerable<Link>> GetAll()
        {
            return await _dataContext.Link.ToListAsync();
        }
            

        public async Task<Link?> GetByAlias(string alias)
        {
            return await _dataContext.Link.Where(l => l.ShortAlias == alias)
                .FirstOrDefaultAsync();
        }

        public async Task<Link?> GetByShortLink(string shortLink)
        {
            return await _dataContext.Link.Where(l => l.ShortLink == shortLink)
                .FirstOrDefaultAsync();
        }

        public async Task<Link?> GetByFullLink(string fullLink)
        {
            return await _dataContext.Link.Where(l => l.FullLink == fullLink)
                .FirstOrDefaultAsync();
        }

        public async Task<Link> CreateNewLink(Link link)
        {
            await _dataContext.Link.AddAsync(link);
            await _dataContext.SaveChangesAsync();
            return link;
        }

        public async Task<Link> IncreaseVisitedCountByAlias(string shortAlias)
        {
            var link = await _dataContext.Link.Where(l => l.ShortAlias == shortAlias)
                .FirstOrDefaultAsync();
            if (link == null)
            {
                link.VisitedCount = link.VisitedCount + 1;
                link.UpdatedAt = System.DateTime.Now;
                await _dataContext.SaveChangesAsync();
            }
           
            return link;
        }
    }
}