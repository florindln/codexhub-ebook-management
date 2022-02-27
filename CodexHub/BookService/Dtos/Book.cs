using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Dtos
{
    public record BookDto(Guid Id, string Title, string Author, string Description, double InitialPrice);
    public record CreateBookDto(string Title, string Author, string Description, double InitialPrice);
    public record UpdateBookDto(double InitialPrice);
}
