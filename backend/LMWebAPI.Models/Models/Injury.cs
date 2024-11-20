using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LMWebAPI.Models;

public class Injury
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    [BsonElement("_id")]
    public ObjectId Id { get; set; }

    [BsonElement("code")]
    public string Code { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("stat_modifiers")]
    public PlayerStats StatModifiers { get; set; }
}