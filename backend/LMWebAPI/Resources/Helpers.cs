using MongoDB.Bson;

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
}