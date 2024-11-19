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
    public PlayerPositionEnum Position { get; set; }

    [BsonElement("stats")]
    public PlayerStats Stats { get; set; }

    [BsonElement("skills")]
    public List<string> Skills { get; set; }

    [BsonElement("injuries")]
    public List<string> Injuries { get; set; }
    
    [BsonElement("spp_available")]
    public int SPPAvailable { get; set; }
    
    [BsonElement("spp_spent")]
    public int SPPSpent { get; set; }
    
    [BsonElement("number")]
    public int Number { get; set; }
    
    [BsonElement("list_position")]
    public List<string> ListPosition { get; set; }
    
    [BsonElement("next_injury_modifier")]
    public bool NextInjuryModifier { get; set; }
}