using MongoDB.Bson;
namespace LMWebAPI.Repositories.Interfaces;

// Base for all CRUD repositories
public interface IRepository<T>
{
    Task<T> GetByIdAsync(ObjectId id);
    Task<List<T>> GetAllAsync();
    Task AddOneAsync(T entity);
    Task ReplaceOneAsync(T replacement);
    Task ReplaceManyAsync(List<T> replacementList);
    Task DeleteOneAsync(ObjectId id);
}