using EcoTrueke.Domain.Interfaces.Repositories;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace EcoTrueke.Infrastructure.Repositories
{
    public class MongoDatabaseRepository : IDatabaseRepository
    {
        private readonly IMongoDatabase _database;

        public MongoDatabaseRepository(IMongoDatabase database)
        {
            _database = database;
        }

        private IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }

        public Task DeleteOneAsync<T>(Expression<Func<T, bool>> filter)
        {
            var collection = GetCollection<T>();
            return collection.DeleteOneAsync(filter);
        }

        public Task<IEnumerable<T>> FindManyAsync<T>(Expression<Func<T, bool>> filter)
        {
            var collection = GetCollection<T>();
            return Task.FromResult<IEnumerable<T>>(collection.Find(filter).ToList());
        }

        public Task<T> FindOneAsync<T>(Expression<Func<T, bool>> filter)
        {
            var collection = GetCollection<T>();
            return collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<T> InsertOneAsync<T>(T document)
        {
            var collection = GetCollection<T>();
            await collection.InsertOneAsync(document);
            return document;
        }

        public async Task ReplaceOneAsync<T>(Expression<Func<T, bool>> filter, T document)
        {
            var collection = GetCollection<T>();
            await collection.ReplaceOneAsync(filter, document);
        }

        public async Task UpdateOneAsync<T>(Expression<Func<T, bool>> filter, Action<T> updateAction)
        {
            var collection = GetCollection<T>();
            var document = await collection.Find(filter).FirstOrDefaultAsync();

            if (document != null)
            {
                updateAction(document);
                await collection.ReplaceOneAsync(filter, document);
            }
        }
    }
}
