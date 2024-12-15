using LMWebAPI.Repositories.Interfaces;
using LMWebAPI.Resources;
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
    
    public async Task<bool> ReplaceOneAsync(T replacement)
    {
        try
        {
            // Will throw ArgumentException if id property cannot be verified.
            ObjectId id = Helpers.CheckIdExistsInDocument(replacement);
            
            var filter = Builders<T>.Filter.Eq("_id", id);
            var result = await Collection.ReplaceOneAsync(filter, replacement);

            if (result.MatchedCount.Equals(0))
            {
                throw new InvalidOperationException("No document matched the filter.");
            }
            
            if (result.ModifiedCount.Equals(0))
            {
                throw new NoChangeException("Document was found but no changes were made.");
            }
        }
        catch (MongoWriteException mwx)
        {
            throw new DatabaseException(
                $"A write error occurred."
                + $"\nMongoWriteException message: {mwx.Message}");
        }
        catch (Exception ex)
        {
            throw;
        }
        
        return true;
    }

    public async Task<bool> ReplaceManyAsync(List<T> replacementList)
    {
        Dictionary <ObjectId, T> replacementsDict = new Dictionary <ObjectId, T>();
        
        // Validate replacements
        foreach (var player in replacementList)
        {
            ObjectId id = Helpers.CheckIdExistsInDocument(player);
            replacementsDict.Add(id, player);
        }
        
        try
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var result = await Collection.ReplaceOneAsync(filter, replacementList);

            if (result.MatchedCount.Equals(0))
            {
                throw new InvalidOperationException("No document matched the filter. Did not perform replacement.");
            }
            
            if (result.ModifiedCount.Equals(0))
            {
                throw new NoChangeException("Document was found but no changes were made.");
            }
        }
        catch (MongoWriteException mwx)
        {
            throw new DatabaseException(
                $"A write error occurred while replacing the Player {id}. Did not perform replacement."
                + $"\nMongoWriteException message: {mwx.Message}");
        }
        catch (Exception ex)
        {
            throw;
        }
        
        return true;
    }
    
    public async Task<bool> DeleteAsync(ObjectId id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await Collection.DeleteOneAsync(filter);
    }
}