using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LMWebAPI.Models;

public class Player
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("team_id")]
    public ObjectId TeamId { get; set; }  // This should reference the team the player is on.

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("position")]
    public string Position { get; set; }

    [BsonElement("stats")]
    public PlayerStats Stats { get; set; }

    [BsonElement("skills")]
    public List<string> Skills { get; set; }

    [BsonElement("injuries")]
    public List<Injury> Injuries { get; set; }
    
    [BsonElement("star_player_points")]
    public int StarPlayerPoints { get; set; }
    
    [BsonElement("number")]
    public int Number { get; set; }
    
    [BsonElement("list_position")]
    public List<string> ListPosition { get; set; }
}

public class Injury
{
    [BsonElement("type")]
    public string Type { get; set; }
    
    [BsonElement("stat_modifier")]
    public PlayerStats StatModifier { get; set; }
}