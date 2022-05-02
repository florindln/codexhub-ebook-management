using BookService.Dtos;
using BookService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.AppServices
{
    public class IncompleteBookEntityFactory : IBookEntityFactory
    {
        public BookEntity Create()
        {
            return new BookEntity
            {
                Id = Guid.Empty,
                Title = "",
                Authors = new List<Author>(),
                Description = "",
                Category = "",
                InitialPrice = 0,
                PageCount = 0,
                PublishedDate = DateTime.MinValue,
                ThumbnailURL = "",
            };
        }
    }
}
