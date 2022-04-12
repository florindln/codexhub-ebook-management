using BookService.Entities;
using CodexhubCommon;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookService.Data
{
    public class BookRepository : IRepository<BookEntity>
    {
        private readonly BookDbContext bookDbContext;

        public BookRepository(BookDbContext bookDbContext)
        {
            this.bookDbContext = bookDbContext;
        }

        public async Task CreateAsync(BookEntity entity)
        {
            bookDbContext.Add(entity);
            await bookDbContext.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(IList<BookEntity> entities)
        {
            bookDbContext.AddRange(entities);
            await bookDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            BookEntity book = new BookEntity { Id = id };
            bookDbContext.Remove(book);
            await bookDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<BookEntity>> GetAllAsync()
        {
            return await bookDbContext.Books.Include(book => book.Authors).ToListAsync();
        }

        public async Task<IReadOnlyCollection<BookEntity>> GetAllAsync(Expression<Func<BookEntity, bool>> filter)
        {
            return await bookDbContext.Books.Include(book => book.Authors).Where(filter).ToListAsync();
        }

        public async Task<BookEntity> GetAsync(Guid id)
        {
            var book = await bookDbContext.Books.Include(book => book.Authors).FirstOrDefaultAsync(x => x.Id == id);
            return book;
        }

        public async Task<BookEntity> GetAsync(Expression<Func<BookEntity, bool>> filter)
        {
            return await bookDbContext.Books.Include(book => book.Authors).Where(filter).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(BookEntity entity)
        {
            bookDbContext.Update(entity);
            await bookDbContext.SaveChangesAsync();
        }
    }
}
