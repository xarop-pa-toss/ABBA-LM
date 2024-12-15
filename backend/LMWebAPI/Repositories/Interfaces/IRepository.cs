using MongoDB.Bson;

namespace LMWebAPI.Repositories.Interfaces;

// Base for all CRUD repositories
public interface IRepository<T>
{
    Task<T?> GetByIdAsync(ObjectId id);
    Task<List<T>> GetAllAsync();
    Task<bool> AddAsync(T entity);
    Task<bool> UpdateAsync(ObjectId Id, T entity);
    Task<bool> DeleteAsync(ObjectId Id);
}
