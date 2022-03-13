using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Dtos
{
    public record BookUserDto(Guid bookId, DateTime boughtAt);
    public record GrantBookDto(Guid UserId, Guid BookId);
}
