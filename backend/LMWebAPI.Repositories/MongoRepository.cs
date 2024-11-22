using LMWebAPI.Models;
using LMWebAPI.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LMWebAPI.Repositories;

public class MongoRepository<T> : IRepository<T>
{
    public readonly IMongoCollection<T> _collection;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<List<T?>> GetAllAsync()
    {       
        return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync(); 
    }

    public async Task<T?> GetByIdAsync(ObjectId id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(ObjectId id, T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(ObjectId id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await _collection.DeleteOneAsync(filter);
    }
}