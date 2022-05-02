using BookService.Conversions;
using BookService.Data;
using BookService.Dtos;
using BookService.Entities;
using CodexhubCommon;
using Newtonsoft.Json;
using StackExchange.Redis;
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
        private readonly IDatabase redisdatabase;

        public BookApp(IBookRepository repository, IDatabase redisdatabase)
        {
            this.repository = repository;
            this.redisdatabase = redisdatabase;
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
            var cachedBook = await redisdatabase.StringGetAsync(id.ToString());

            if (cachedBook.IsNullOrEmpty)
            {
                var book = await repository.GetAsync(id);

                if (book == null)
                    return null;

                await redisdatabase.StringSetAsync(book.Id.ToString(), JsonConvert.SerializeObject(book));
                return book;
            }

            return JsonConvert.DeserializeObject<BookEntity>(cachedBook.ToString());
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

        public async Task CreateBook(BookEntity book)
        {
            var existingBook = await repository.GetAsync(existing => existing.Title == book.Title);
            if (existingBook != null)
                throw new ArgumentException($"Book with title {book.Title} already exists");

            await repository.CreateAsync(book);
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
            await redisdatabase.StringSetAsync(updatedBook.Id.ToString(), JsonConvert.SerializeObject(updatedBook));

            return updatedBook;
        }

        public async Task DeleteBook(Guid id)
        {
            await redisdatabase.StringGetDeleteAsync(id.ToString());
            await repository.DeleteAsync(id);
        }
    }
}
