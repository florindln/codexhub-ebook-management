using BookService.Dtos;
using BookService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.AppServices
{
    public class NormalBookEntityFactory : IBookEntityFactory
    {
        private readonly BookEntity book;
        public NormalBookEntityFactory(CreateBookDto bookDto)
        {
            book = new BookEntity
            {
                Id = Guid.NewGuid(),
                Authors = bookDto.Authors.Select(author => new Author { Name = author }).ToList(),
                Category = bookDto.Category,
                Description = bookDto.Description,
                InitialPrice = bookDto.InitialPrice,
                PageCount = bookDto.PageCount,
                PublishedDate = bookDto.PublishedDate,
                ThumbnailURL = bookDto.ThumbnailURL,
                Title = bookDto.Title,
            };
        }

        public BookEntity Create()
        {
            return book;
        }
    }
}
