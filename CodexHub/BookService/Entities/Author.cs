using System;
using System.ComponentModel.DataAnnotations;

namespace BookService.Entities
{
    public class Author
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}