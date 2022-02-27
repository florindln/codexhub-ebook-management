using BookService.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Repositories
{
    public class BookRepository
    {
        private const string collectionName = "books";
        private readonly IMongoCollection<BookEntity> dbCollection;
        private readonly FilterDefinitionBuilder<BookEntity> filterBuilder = Builders<BookEntity>.Filter;

        public BookRepository()
        {
            var mongoclient = new MongoClient("mongodb://localhost:27017");
        }
    }
}
