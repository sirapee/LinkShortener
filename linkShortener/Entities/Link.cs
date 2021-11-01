using System;

namespace LinkShortener.Entities
{
    public class Link
    {

        public string Id { get; set; }

        public string ShortAlias { get; set; }

        public string FullLink { get; set; }

        public string ShortLink { get; set; }

        public int VisitedCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}