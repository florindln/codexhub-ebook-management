using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Dtos
{
    public record BookDto(Guid id, string title, string author, string description, float initialPrice);
    public record UpdateBookDto(Guid id, float initialPrice);
}
