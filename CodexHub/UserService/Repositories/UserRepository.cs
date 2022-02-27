using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Conversion;
using UserService.Entities;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string collectionName = "users";
        private readonly IMongoCollection<UserEntity> dbCollection;
        private readonly FilterDefinitionBuilder<UserEntity> filterBuilder = Builders<UserEntity>.Filter;

        public UserRepository(IMongoDatabase mongoDatabase)
        {
            dbCollection = mongoDatabase.GetCollection<UserEntity>(collectionName);
        }

        public async Task<IReadOnlyCollection<UserEntity>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<UserEntity> GetAsync(Guid id)
        {
            var filter = filterBuilder.Eq(x => x.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(UserEntity user)
        {
            user.NotNull();

            await dbCollection.InsertOneAsync(user);
        }

        public async Task UpdateAsync(UserEntity user)
        {
            user.NotNull();

            var filter = filterBuilder.Eq(x => x.Id, user.Id);

            await dbCollection.ReplaceOneAsync(filter, user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = filterBuilder.Eq(x => x.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }

    }
}
