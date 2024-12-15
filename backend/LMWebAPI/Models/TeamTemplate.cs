using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LMWebAPI.Models;

public class TeamTemplate
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("team_codename")]
    public required string TeamCodename { get; set; }

    [BsonElement("player_templates_ids")]
    public List<TeamTemplatePlayer> PlayerTemplatesIds { get; set; } = new List<TeamTemplatePlayer>();
}

public class TeamTemplatePlayer
{
    ObjectId player_template_id { get; set; }
    int list_position { get; set; }
}