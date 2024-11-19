using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LMWebAPI.Models;

public class TeamTemplate
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("user_id")]
    public ObjectId UserId { get; set; }  // This references the user (coach) owning the team.

    [BsonElement("team_type")]
    public string TeamType { get; set; }

    [BsonElement("player_templates_ids")]
    public List<ObjectId> PlayerTemplatesIds { get; set; }
}