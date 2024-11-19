using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LMWebAPI.Models;

public class PlayerTemplate
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    
    [BsonElement("team_code")]
    public string TeamCode { get; set; }

    [BsonElement("position")]
    public PlayerPositionEnum Position { get; set; }

    [BsonElement("cost")]
    public int Cost { get; set; }

    [BsonElement("stats")]
    public PlayerStats Stats { get; set; }

    [BsonElement("skills")]
    public List<string> Skills { get; set; }
    
    [BsonElement("injuries")]
    public List<string> Injuries { get; set; }
    
    [BsonElement("rank_description")]
    public string RankDescription { get; set; }
}

public class PlayerStats
{
    [BsonElement("movement")]
    public int Movement { get; set; }

    [BsonElement("strength")]
    public int Strength { get; set; }

    [BsonElement("agility")]
    public int Agility { get; set; }

    [BsonElement("armor")]
    public int Armor { get; set; }
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