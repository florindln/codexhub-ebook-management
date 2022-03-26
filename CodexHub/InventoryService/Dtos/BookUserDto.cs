using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Dtos
{
    public record BookUserDto(Guid bookId, DateTime boughtAt, string Title, double InitialPrice);
    public record GrantBookDto(Guid UserId, Guid BookId);
    public record CatalogBookDto(Guid Id, string Title, double InitialPrice);
}
