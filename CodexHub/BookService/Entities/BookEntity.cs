using CodexhubCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Entities
{
    public class BookEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Category { get; set; }
        public string ThumbnailURL { get; set; }
        public double InitialPrice { get; set; }

    }
}
