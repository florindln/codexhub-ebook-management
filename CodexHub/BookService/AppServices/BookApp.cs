using BookService.Conversions;
using BookService.Data;
using BookService.Dtos;
using BookService.Entities;
using CodexhubCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.AppServices
{
    public class BookApp
    {
        private readonly BookRepository repository;

        public BookApp(BookRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooks()
        {
            var bookEntities = await repository.GetAllAsync();
            return bookEntities.Select(bookEntity => bookEntity.AsDto());
        }

        public async Task<BookDto> GetBook(Guid id)
        {
            var book = await repository.GetAsync(id);
            return book.AsDto();
        }

        public async Task CreateBooks(List<BookEntity> books)
        {
            var existingBooks = await this.GetAllBooks();
            var bookNames = existingBooks.Select(x => x.Title);
            var res = new List<BookEntity>();
            foreach (var book in books)
            {
                if (bookNames.Contains(book.Title))
                    continue;

                res.Add(book);
            }
            await repository.CreateRangeAsync(res);
        }

        public async Task<BookDto> UpdateBook(Guid id, UpdateBookDto updateBookDto)
        {
            var existingBook = await this.GetBook(id);

            if (existingBook == null)
                return null;

            var updatedBook = existingBook with
            {
                Description = updateBookDto.Description,
                InitialPrice = updateBookDto.InitialPrice,
            };

            return updatedBook;
        }

        internal Task DeleteBook(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
