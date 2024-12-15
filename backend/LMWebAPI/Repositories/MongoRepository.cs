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
        Dictionary <ObjectId, T> existingDocsDict = new Dictionary <ObjectId, T>();
        
        try
        {
            // Replacements have their IDs validated and then are compared to matching documents in the DB
            // This is done because Mongo Replace functions always run regardless of origin = replacement
            foreach (var replacement in replacementList)
            {
                ObjectId id = Helpers.CheckIdExistsInDocument(replacement);
                replacementsDict.Add(id, replacement);
            }
            
            var existingDocsFilter = Builders<T>.Filter.In("_id", replacementsDict.Keys);
            var existingDocsList = await Collection.Find(existingDocsFilter).ToListAsync();
            existingDocsDict = existingDocsList.ToDictionary(x => Helpers.CheckIdExistsInDocument(x));

            if (!existingDocsDict.Any())
            {
                throw new InvalidOperationException("No documents matched the filter.");
            }

            // Check if new docs values match the ones on the DB
            foreach (var kvp in replacementsDict)
            {
                var id = kvp.Key;
                var replacement = kvp.Value;
                
                if (existingDocsDict.TryGetValue(id, out var existingDoc)) 
                {
                    // Only replace if they don't match
                    if (existingDoc.Equals(replacement)) continue;
                    
                    var replaceFilter = Builders<T>.Filter.Eq("_id", id);
                    await Collection.ReplaceOneAsync(replaceFilter, replacement);
                    Console.WriteLine($"Replaced document with ID: {id}");
                }
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
    
    public async Task<bool> DeleteAsync(ObjectId id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await Collection.DeleteOneAsync(filter);
    }
}