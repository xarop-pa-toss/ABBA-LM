using LMWebAPI.Repositories.Interfaces;
using LMWebAPI.Resources;
using LMWebAPI.Resources.Errors;
using MongoDB.Bson;
using MongoDB.Driver;
namespace LMWebAPI.Repositories;

public class MongoRepository<T> : IRepository<T>
{
    protected readonly IMongoClient Client;
    protected readonly IMongoCollection<T> Collection;
    protected readonly IMongoDatabase Database;

    public MongoRepository(IMongoDatabase database, string collectionName, IMongoClient client)
    {
        Database = database;
        Collection = Database.GetCollection<T>(collectionName);
        Client = client;
    }

    public async Task<List<T>> GetAllAsync()
    {
        var resultList = await Collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        if (resultList == null || resultList.Count == 0)
        {
            throw new Problem404NotFoundException("No results found.");
        }

        return resultList;
    }

    public async Task<T> GetByIdAsync(ObjectId id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        var result = await Collection.Find(filter).FirstOrDefaultAsync();
        if (result == null)
        {
            throw new Problem404NotFoundException("No result found.");
        }

        return result;
    }


    public async Task AddOneAsync(T entity)
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
        try
        {
            // Will throw ArgumentException if id property cannot be verified.
            var id = Helpers.GetIdIfExistsInDocument(replacement);
            var filter = Builders<T>.Filter.Eq("_id", id);

            var operationResult = await Collection.ReplaceOneAsync(filter, replacement);

            if (operationResult.MatchedCount.Equals(0))
            {
                throw new Problem404NotFoundException("No document matched the filter.");
            }

            // if (operationResult.ModifiedCount.Equals(0))
            // {
            //     throw new ProblemDatabaseException("Document was found but no changes were made.");
            // }
        }
        catch (MongoWriteException mwx)
        {
            Helpers.HandleMongoWriteException(mwx);
        }
    }

    public async Task ReplaceManyAsync(List<T> replacementList)
    {
        var replacementsDict = new Dictionary<ObjectId, T>();
        Dictionary<ObjectId, T> existingDocsDict;

        try
        {
            // Replacements have their IDs validated and then are compared to matching documents in the DB
            // Only virtually equal documents are replaced
            // This is done because Mongo Replace functions always run regardless if origin = replacement
            foreach (var replacement in replacementList)
            {
                var id = Helpers.GetIdIfExistsInDocument(replacement);
                replacementsDict.Add(id, replacement);
            }

            var existingDocsFilter = Builders<T>.Filter.In("_id", replacementsDict.Keys);
            var existingDocsList = await Collection.Find(existingDocsFilter).ToListAsync();

            if (!existingDocsList.Any())
            {
                throw new InvalidOperationException("No documents matched the filter.");
            }

            existingDocsDict = existingDocsList.ToDictionary(x => Helpers.GetIdIfExistsInDocument(x));

            // Check if new docs values match the ones on the DB
            ReplaceOneResult? operationResult = null;
            foreach (var kvp in replacementsDict)
            {
                var id = kvp.Key;
                var replacement = kvp.Value;

                if (existingDocsDict.TryGetValue(id, out var existingDoc))
                {
                    // Only replace if they don't match
                    if (existingDoc != null && existingDoc.Equals(replacement)) continue;

                    var replaceFilter = Builders<T>.Filter.Eq("_id", id);
                    operationResult = await Collection.ReplaceOneAsync(replaceFilter, replacement);
                    Console.WriteLine($"Replaced document with ID: {id}");
                }
                else
                {
                    Console.WriteLine($"Did not find document with ID: {id}");
                }
            }

            // if (operationResult == null || operationResult.ModifiedCount.Equals(0))
            // {
            //     throw new ProblemNoChangeException("Document was found but no changes were made.");
            // }
        }
        catch (MongoWriteException mwx)
        {
            Helpers.HandleMongoWriteException(mwx);
        }
    }


    public async Task DeleteOneAsync(ObjectId id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await Collection.DeleteOneAsync(filter);
    }
    public async Task AddManyAsync(IEnumerable<T> docList)
    {
        using var session = await Client.StartSessionAsync();
        {
            var cancellationToken = CancellationToken.None;
            await session.WithTransactionAsync(
                async (sessionHandle, cToken) =>
                {
                    try
                    {
                        foreach (var document in docList)
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            await Collection.InsertOneAsync(sessionHandle, document);
                        }
                    }
                    catch (MongoWriteException mwx)
                    {
                        Helpers.HandleMongoWriteException(mwx);
                        throw;
                    }

                    return true;
                },
                cancellationToken: cancellationToken);
        }
    }
}