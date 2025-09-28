using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LMWebAPI.Models;
public class Team_old
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("user_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string UserId { get; set; }

    [BsonElement("team_codename")]
    public required string TeamCodename { get; set; }

    [BsonElement("players")]
    public List<TeamPlayer> Players { get; set; } = new List<TeamPlayer>();

    [BsonElement("name")]
    public required string Name { get; set; } = "Not Named";
}

public class TeamPlayer
{
    [BsonElement("player_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string PlayerId { get; set; } 

    [BsonElement("list_position")]
    public required int ListPosition { get; set; }
}