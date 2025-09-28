using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace LMWebAPI.Models;

public class Player_old
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("team_id")]
    public required ObjectId TeamId { get; set; }

    [BsonElement("name")]
    public required string Name { get; set; }

    [BsonElement("position")]
    public required PlayerPositionEnum Position { get; set; }

    [BsonElement("stats")]
    public required PlayerStats Stats { get; set; }

    [BsonElement("skills")]
    public List<string> Skills { get; set; } = new List<string>();

    [BsonElement("injuries")]
    public List<string> Injuries { get; set; } = new List<string>();

    [BsonElement("spp_available")]
    public required int SppAvailable { get; set; }

    [BsonElement("spp_spent")]
    public required int SppSpent { get; set; }

    [BsonElement("rank")]
    public required string Rank { get; set; }

    [BsonElement("value")]
    public required int Value { get; set; }

    [BsonElement("number")]
    public int Number { get; set; }
}