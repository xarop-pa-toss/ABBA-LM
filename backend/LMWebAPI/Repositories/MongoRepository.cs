using LMWebAPI.Repositories.Interfaces;
using LMWebAPI.Resources;
using LMWebAPI.Resources.Errors;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        var resultList = await Collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        if (resultList == null || resultList.Count == 0)
        {
            throw new NotFoundException("No results found.");
        }
        
        return resultList;
    }

    public async Task<T?> GetByIdAsync(ObjectId id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        var result = await Collection.Find(filter).FirstOrDefaultAsync();
        if (result == null)
        {
            throw new NotFoundException("No result found.");
        }

        return result;
    }

    public async Task AddAsync(T entity)
    {
        try
        {
            await Collection.InsertOneAsync(entity);
        }
        catch (MongoWriteException mwx)
        {
            Helpers.HandleMongoWriteException(mwx);
        }
    }
    
    public async Task ReplaceOneAsync(T replacement)
    {
        // Will throw ArgumentException if id property cannot be verified.
        ObjectId id = Helpers.CheckIdExistsInDocument(replacement);
        
        var filter = Builders<T>.Filter.Eq("_id", id);
        
        try
        {
            var operationResult = await Collection.ReplaceOneAsync(filter, replacement);
        
            if (operationResult.MatchedCount.Equals(0))
            {
                throw new NotFoundException("No document matched the filter.");
            }
            
            if (operationResult.ModifiedCount.Equals(0))
            {
                throw new RepositoryError("Document was found but no changes were made.");
            }
        }
        catch (MongoWriteException mwx)
        {
            Helpers.HandleMongoWriteException(mwx);
        }
    }

    public async Task ReplaceManyAsync(List<T> replacementList)
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
            Helpers.HandleMongoWriteException(mwx);
        }
    }

    public async Task<bool> DeleteAsync(ObjectId id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await Collection.DeleteOneAsync(filter);
    }
}