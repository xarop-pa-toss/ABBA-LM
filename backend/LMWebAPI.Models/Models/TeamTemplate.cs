using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LMWebAPI.Models;

public class TeamTemplate
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("team_code")]
    public string TeamCode { get; set; }

    [BsonElement("player_templates_ids")]
    public List<TeamTemplatePlayer> PlayerTemplatesIds { get; set; }
}

public class TeamTemplatePlayer
{
    ObjectId player_id { get; set; }
    int list_position { get; set; }
}