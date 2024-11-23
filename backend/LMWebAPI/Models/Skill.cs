using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LMWebAPI.Models;

public class Skill
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    
    [BsonElement("type")]
    public required string Type { get; set; }
    
    [BsonElement("code")]
    public required string Code { get; set; }
    
    [BsonElement("name")]
    public required string Name { get; set; }
    
    [BsonElement("is_special_trait")]
    public required bool IsSpecialTrait { get; set; }
    
    [BsonElement("description")]
    public string Description { get; set; }
}