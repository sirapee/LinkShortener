using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LinkShortener.Entities;
using LinkShortener.Repositories;
using LinkShortener.Services;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

namespace LinkShortener.ServiceImplementation
{
    public class LinkService : ILinkService
    {
        private readonly IShortenerService _shortenerService;
        private readonly ILinkRepository _linkRepository;
        private static string _domainName;

        public LinkService(
            IShortenerService shortenerService,
            ILinkRepository linkRepository, IConfiguration configuration)
        {
            _shortenerService = shortenerService;
            _linkRepository = linkRepository;
            _domainName = configuration.GetValue<string>("DomainName");
        }
        
        public async Task<string> GetShortAlias(string fullLink)
        {
            var existedLink = await _linkRepository.GetByFullLink(fullLink);

            return existedLink?.ShortAlias 
                ?? await GenerateShortAlias(fullLink);
        }

        public async Task<Link?> GetDetailsByAlias(string shortAlias)
        {
            return await _linkRepository.GetByAlias(shortAlias);
        }

        public async Task<string> GetShortLink(string fullLink)
        {
            var existedLink = await _linkRepository.GetByFullLink(fullLink);

            return existedLink?.ShortAlias
                ?? await GenerateShortAlias(fullLink);
        }

        public async Task<string?> GetFullLink(string shortLink)
        {
            var link =  await _linkRepository.GetByShortLink(shortLink);
            return link?.FullLink;


        }

        public async Task<string?> GetFullLinkAndIncreaseVisitCount(string shortAlias)
        {
            var link = await _linkRepository.IncreaseVisitedCountByAlias(shortAlias);

            return link?.FullLink;
        }

        public Task<IEnumerable<Link>> GetAllLinks()
        {
            return _linkRepository.GetAll();
        }

        // 'GetHashCode() for String' does not guarantee unique code for different strings, but, nevertheless, the chance of collisions is not so high.
        // So, I use it and try to write a value to the DB, given the presence of an index in the database,
        // which will throw an error  if will getting collision and code will try again with a new id.
        private async Task<string> GenerateShortAlias(string fullLink)
        {
            var tryCount = 0;

            while (tryCount < 3)
            {
                try
                {

                    string id = Guid.NewGuid().ToString();
                    var shortAlias = _shortenerService.GenerateShortString();
                    var shortLink = _domainName + shortAlias;
                    var newLink = new Link
                    {
                        FullLink = fullLink,
                        UpdatedAt = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow,
                        Id = id,
                        ShortAlias = shortAlias,
                        ShortLink = shortLink
                    };

                    await _linkRepository.CreateNewLink(newLink);
                
                    return newLink.ShortLink;
                }
                catch (MongoDB.Driver.MongoDuplicateKeyException)
                {
                    tryCount++;
                }
            }
            
            throw new InvalidOperationException("Three tries not enough for resolve collisions!");
        }
    }
}