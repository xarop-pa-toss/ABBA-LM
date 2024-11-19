using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LMWebAPI.Models;

public class PlayerTemplate
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    
    [BsonElement("team_type")]
    public string TeamType { get; set; }

    [BsonElement("position")]
    public string Position { get; set; }

    [BsonElement("cost")]
    public int Cost { get; set; }

    [BsonElement("stats")]
    public PlayerStats Stats { get; set; }

    [BsonElement("skills")]
    public List<string> Skills { get; set; }
    
    [BsonElement("injuries")]
    public List<string> Injuries { get; set; }
    
    [BsonElement("star_player_points")]
    public int StarPlayerPoints { get; set; }
    
    [BsonElement("star_player_points_used")]
    public int StarPlayerPointsUsed { get; set; }
    
    [BsonElement("rank_description")]
    public string RankDescription { get; set; }
    
    [BsonElement("number")]
    public int Number { get; set; }
    
    [BsonElement("list_position")]
    public List<string> ListPosition { get; set; }
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