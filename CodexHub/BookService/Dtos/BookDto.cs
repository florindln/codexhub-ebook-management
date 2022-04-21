using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Dtos
{
    public record BookDto(Guid Id, string Title, List<string> Authors, string Description,
         int PageCount, DateTime PublishedDate, string Category, string ThumbnailURL, double InitialPrice);
    public record CreateBookDto(string Title, List<string> Authors, string Description,
         int PageCount, DateTime PublishedDate, string Category, string ThumbnailURL, double InitialPrice);

    public record UpdateBookDto(double InitialPrice, string Description);
}
