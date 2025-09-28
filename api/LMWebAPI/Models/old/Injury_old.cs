using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace LMWebAPI.Models;

public class Injury_old
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    [BsonElement("_id")]
    public ObjectId Id { get; set; }

    [BsonElement("code")]
    public required string Code { get; set; }

    [BsonElement("name")]
    public required string Name { get; set; }

    [BsonElement("description")]
    public required string Description { get; set; }

    [BsonElement("stat_modifiers")]
    public required PlayerStats StatModifiers { get; set; }
}