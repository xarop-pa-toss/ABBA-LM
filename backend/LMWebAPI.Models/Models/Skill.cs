using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LMWebAPI.Models;

public class Skill
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("type")]
    public string Type { get; set; }
    
    [BsonElement("code")]
    public string Code { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("is_special_trait")]
    public string IsSpecialTrait { get; set; }
    
    [BsonElement("description")]
    public string Description { get; set; }
}