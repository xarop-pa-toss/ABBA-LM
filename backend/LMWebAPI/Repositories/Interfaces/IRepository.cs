using MongoDB.Bson;

namespace LMWebAPI.Repositories.Interfaces;

// Base for all CRUD repositories
public interface IRepository<T>
{
    Task<T?> GetByIdAsync(ObjectId id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(ObjectId Id, T entity);
    Task DeleteAsync(ObjectId Id);
}
