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
        public async Task<IActionResult> GetShortLink(string fullLink)
        {
            var shortLink =  await _linkService.GetShortLink(fullLink);
            return Ok(new
            {
                isSuccessful = true,
                shortLink = shortLink
            }); 
 
        }

        [HttpPost("api/decode")]
        public async Task<IActionResult> GetFullLink(string shortLink)
        {
            var fullLink = await _linkService.GetFullLink(shortLink);

            return Ok(new
            {
                isSuccessful = true,
                fullLink = fullLink
            }); 
        }

        [HttpGet("api/list")]
        public async Task<IActionResult> GetAllLinks()
        {
            var links = await _linkService.GetAllLinks();
            return Ok(new
            {
                isSuccessful = true,
                links = links.Select(l => new { ShortLink = l.ShortLink, l.VisitedCount, l.ShortAlias, l.FullLink })
             });
        }

        [HttpGet("api/statistics/{urlPath}")]
        public async Task<IActionResult> Statistics(string urlPath)
        {
            var link = await _linkService.GetDetailsByAlias(urlPath);

            return Ok(new
            {
                isSuccessful = true,
                link = new
                {
                    VisitedCount = link.VisitedCount,
                    CreatedDate = link.CreatedAt,
                    ShortLink = link.ShortLink,
                    FullLink = link.FullLink,
                }
            });
        }

        [HttpGet("/{shortAlias}")]
        public async Task<IActionResult> RedirectFromShort(string shortAlias)
        {
            var fullLink = await _linkService.GetFullLinkAndIncreaseVisitCount(shortAlias);

            if (fullLink == null)
            {
                return NotFound(new
                {
                    isSuccessful = false,
                    fullLink = $"Not found link by alias '{shortAlias}'"
                });
            }

            return Ok(new
            {
                isSuccessful = true,
                fullLink = fullLink
            });
        }
    }
}