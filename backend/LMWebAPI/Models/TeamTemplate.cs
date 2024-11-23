using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LMWebAPI.Models;

public class TeamTemplate
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("team_code")]
    public required string TeamCode { get; set; }

    [BsonElement("player_templates_ids")]
    public List<TeamTemplatePlayer> PlayerTemplatesIds { get; set; } = new List<TeamTemplatePlayer>();
}

public class TeamTemplatePlayer
{
    ObjectId player_id { get; set; }
    int list_position { get; set; }
}