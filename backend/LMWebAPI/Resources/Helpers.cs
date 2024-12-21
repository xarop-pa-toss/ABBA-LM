using LMWebAPI.Resources.Errors;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LMWebAPI.Resources;

public static class Helpers
{
    public static ObjectId CheckIdExistsInDocument<T>(T doc)
    {
        var idPropInfo = typeof(T).GetProperty("_id");
        if (idPropInfo == null)
        {
            throw new ArgumentException($"Property '_id' is not set in type {typeof(T)}.");
        }

        var idValue = idPropInfo.GetValue(doc);
        if (idValue == null)
        {
            throw new ArgumentException($"'_id' is null or empty.");
        }
        
        return (ObjectId)idValue;
    }

    public static void HandleMongoWriteException(MongoWriteException mwx)
    {
        switch (mwx.WriteError.Category)
        {
            case ServerErrorCategory.DuplicateKey:
                throw new ConflictException("Duplicate entity found.");
            case ServerErrorCategory.ExecutionTimeout:
                throw new RepositoryError("Database interaction timed out.");
            case ServerErrorCategory.Uncategorized:
                throw new RepositoryError("Database error. Could not add entity.");
        }
    }
}