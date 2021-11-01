using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

using LinkShortener.Services;

namespace LinkShortener.Controllers
{
    [ApiController]
    public class LinkController : ControllerBase
    {
        private readonly ILinkService _linkService;
        private readonly string _domainName;
        private string _userId =>
            HttpContext.Session.GetString("UserId");
        
        public LinkController(ILinkService linkService,
            IConfiguration configuration)
        {
            _linkService = linkService;
            _domainName = configuration.GetValue<string>("DomainName");
        }
        
        [HttpPost("api/encode")]
        public async Task<string> GetShortLink(string fullLink)
        {
            return await _linkService.GetShortLink(fullLink);
 
        }

        [HttpPost("api/decode")]
        public async Task<string> GetFullLink(string shortLink)
        {
            return await _linkService.GetFullLink(shortLink);

        }

        [HttpGet("api/list")]
        public async Task<IEnumerable<dynamic>> GetAllLinks()
        {
            var links = await _linkService.GetAllLinks();

            return links.Select(l => new { ShortLink = l.ShortLink, l.VisitedCount, l.ShortAlias, l.FullLink});
        }

        [HttpGet("api/statistics/{urlPath}")]
        public async Task<dynamic> Statistics(string urlPath)
        {
            var link = await _linkService.GetDetailsByAlias(urlPath);

            return  new
            {
                VisitedCount = link.VisitedCount,
                CreatedDate = link.CreatedAt,
                ShortLink = link.ShortLink,
                FullLink = link.FullLink,
            };
        }

        [HttpGet("/{shortAlias}")]
        public async Task<IActionResult> RedirectFromShort(string shortAlias)
        {
            var fullLink = await _linkService.GetFullLinkAndIncreaseVisitCount(shortAlias);

            if (fullLink == null)
            {
                return NotFound($"Not found link by alias '{shortAlias}'");
            }

            return Redirect(fullLink);
        }
    }
}