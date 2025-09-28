using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace LMWebAPI.Models;

public class PlayerTemplate_old
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("team_code")]
    public required string TeamCode { get; set; }

    [BsonElement("position")]
    public required PlayerPositionEnum Position { get; set; }

    [BsonElement("cost")]
    public required int Cost { get; set; }

    [BsonElement("stats")]
    public required PlayerStats Stats { get; set; }

    [BsonElement("skills")]
    public List<string> Skills { get; set; } = new List<string>();

    [BsonElement("injuries")]
    public List<string> Injuries { get; set; } = new List<string>();

    [BsonElement("rank_description")]
    public required string RankDescription { get; set; }
}

public class PlayerStats
{
    [BsonElement("movement")]
    public required int Movement { get; set; }

    [BsonElement("strength")]
    public required int Strength { get; set; }

    [BsonElement("agility")]
    public required int Agility { get; set; }

    [BsonElement("armor")]
    public required int Armor { get; set; }
}

public enum PlayerPositionEnum
{
    Blitzer,
    Thrower,
    Catcher,
    Lineman,
    StarPlayer
}

public class Rank
{
}