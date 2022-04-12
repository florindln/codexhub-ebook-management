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
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public List<Author> Authors { get; set; } = new List<Author>();

        [Required]
        [MaxLength(10000)]
        public string Description { get; set; }

        [Required]
        public int PageCount { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Category { get; set; }

        [Required]
        [MaxLength(200)]
        public string ThumbnailURL { get; set; }

        [Required]
        public double InitialPrice { get; set; }
    }
}
