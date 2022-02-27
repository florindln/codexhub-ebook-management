using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Dtos
{
    public record BookDto(Guid Id, [Required] string Title, [Required] string Author, [Required] string Description,
        [Required] double InitialPrice);
    public record CreateBookDto([Required] string Title, [Required] string Author, [Required] string Description,
        [Required][Range(0, double.MaxValue)] double InitialPrice);
    public record UpdateBookDto([Range(0, double.MaxValue)] double InitialPrice);
}
