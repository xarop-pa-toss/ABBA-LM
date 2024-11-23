using LMWebAPI.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LMWebAPI.Repositories;

public class MongoRepository<T> : IRepository<T>
{
    protected readonly IMongoDatabase Database;
    protected readonly IMongoCollection<T> Collection;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        Database = database;
        Collection = Database.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllAsync()
    {       
        return await Collection.Find(Builders<T>.Filter.Empty).ToListAsync(); 
    }

    public async Task<T?> GetByIdAsync(ObjectId id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task AddAsync(T entity)
    {
        await Collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(ObjectId id, T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await Collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(ObjectId id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await Collection.DeleteOneAsync(filter);
    }
}