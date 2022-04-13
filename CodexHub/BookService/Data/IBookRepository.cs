using BookService.Entities;
using CodexhubCommon;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookService.Data
{
    public interface IBookRepository : IRepository<BookEntity>
    {
        Task CreateRangeAsync(IList<BookEntity> entities);
    }
}