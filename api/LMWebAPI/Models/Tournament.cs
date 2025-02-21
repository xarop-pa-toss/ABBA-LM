using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LMWebAPI.Models;

public class Tournament
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    
    // Meta
    [BsonElement("creator_user_id")]
    public required string CreatorUserId { get; set; }
    
    [BsonElement("creation_date")]
    public required DateTime CreationDate { get; set; }
    
    [BsonElement("start_datetime")]
    public required DateTime StartDate { get; set; }
    
    
    // General Properties
    [BsonElement("name")]
    public required string Name { get; set; }
    
    [BsonElement("team_rating_max")]
    public int TeamRatingMax { get; set; }
    
    [BsonElement("number_of_teams")]
    public int NumberOfTeams { get; set; }
    
    [BsonElement("registered_coach_ids")]
    public List<ObjectId> RegisteredCoachIds { get; set; }
    
    
    // Groups
    // [BsonElement("Groups")]
    // public CoachGroups CoachGroups { get; set; }
    
    
    
}