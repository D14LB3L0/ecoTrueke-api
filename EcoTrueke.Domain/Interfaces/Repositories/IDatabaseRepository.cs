using System.Linq.Expressions;

namespace EcoTrueke.Domain.Interfaces.Repositories
{
    public interface IDatabaseRepository
    {
        Task<T> FindOneAsync<T>(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> FindManyAsync<T>(Expression<Func<T, bool>> filter);
        Task<T> InsertOneAsync<T>(T document);
        Task UpdateOneAsync<T>(Expression<Func<T, bool>> filter, Action<T> updateAction);
        Task DeleteOneAsync<T>(Expression<Func<T, bool>> filter);
        Task ReplaceOneAsync<T>(Expression<Func<T, bool>> filter, T document);
    }
}
