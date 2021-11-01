using System.Collections.Generic;
using System.Threading.Tasks;

using LinkShortener.Entities;

namespace LinkShortener.Services
{
    /// <summary>
    /// Service for interaction with link
    /// </summary>
    public interface ILinkService
    {
        /// <summary>
        /// Get short alias for shirt url
        /// </summary>
        /// <param name="fullLink"></param>
        /// <returns></returns>
        Task<string> GetShortAlias(string fullLink);

        /// <summary>
        /// Get short alias for shirt url
        /// </summary>
        /// <param name="fullLink"></param>
        /// <returns></returns>
        Task<string> GetShortLink(string fullLink);

        /// <summary>
        /// Get short alias for shirt url
        /// </summary>
        /// <param name="shortAlias"></param>
        /// <returns></returns>
        Task<Link> GetDetailsByAlias(string shortAlias);

        /// <summary>
        /// Get short alias for shirt url
        /// </summary>
        /// <param name="shortLink"></param>

        /// <returns></returns>
        Task<string> GetFullLink(string shortLink);

        /// <summary>
        /// Get full link and increase visit count
        /// </summary>
        /// <param name="shortAlias"></param>
        /// <returns>Return 'null' if not found.</returns>
        Task<string?> GetFullLinkAndIncreaseVisitCount(string shortAlias);

        /// <summary>
        /// Get all existed links for user
        /// </summary>
        /// /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Link>> GetAllLinks();
    }
}