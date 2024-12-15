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

    public async Task<bool> AddAsync(T entity)
    {
        try
        {
            await Collection.InsertOneAsync(entity);
            return true;
        }
        catch (MongoWriteException mwx) when (mwx.WriteError.Category == ServerErrorCategory.DuplicateKey)
        {
            Console.WriteLine("Duplicate key error: " + mwx.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            return false;
        }
    }
    
    public async Task<bool> ReplaceDocumentAsync(ObjectId id, T replacement)
    {
        try
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var result = await Collection.ReplaceOneAsync(filter, replacement);

            if (result.MatchedCount.Equals(0))
            {
                throw new Exception("No document matched the filter. Did not perform replacement.");
            }

            return result.ModifiedCount > 0;
        }
        catch ()
    }

    public async Task DeleteAsync(ObjectId id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await Collection.DeleteOneAsync(filter);
    }
}