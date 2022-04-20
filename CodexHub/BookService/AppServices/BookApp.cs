using BookService.Conversions;
using BookService.Data;
using BookService.Dtos;
using BookService.Entities;
using CodexhubCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookService.AppServices
{
    public class BookApp
    {
        private readonly IBookRepository repository;

        public BookApp(IBookRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooks()
        {
            var bookEntities = await repository.GetAllAsync();
            return bookEntities.Select(bookEntity => bookEntity.AsDto());
        }

        public async Task<IEnumerable<BookDto>> GetAllBooks(Expression<Func<BookEntity, bool>> filter)
        {
            var bookEntities = await repository.GetAllAsync(filter);
            return bookEntities.Select(bookEntity => bookEntity.AsDto());
        }

        public async Task<BookEntity> GetBook(Guid id)
        {
            var book = await repository.GetAsync(id);
            return book;
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

        public async Task<BookEntity> UpdateBook(Guid id, UpdateBookDto updateBookDto)
        {
            var existingBook = await this.GetBook(id);

            if (existingBook == null)
                return null;

            var updatedBook = existingBook;
            updatedBook.Description = updateBookDto.Description;
            updatedBook.InitialPrice = updateBookDto.InitialPrice;

            await repository.UpdateAsync(updatedBook);

            return updatedBook;
        }

        public async Task DeleteBook(Guid id)
        {
            await repository.DeleteAsync(id);
        }
    }
}
