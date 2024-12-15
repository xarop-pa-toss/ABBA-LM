using MongoDB.Bson;

namespace LMWebAPI.Repositories.Interfaces;

// Base for all CRUD repositories
public interface IRepository<T>
{
    Task<T?> GetByIdAsync(ObjectId id);
    Task<List<T>> GetAllAsync();
    Task<bool> AddOneAsync(T entity);
    Task<bool> ReplaceOneAsync(ObjectId id, T entity);
    Task<bool> DeleteOneAsync(ObjectId id);
}
