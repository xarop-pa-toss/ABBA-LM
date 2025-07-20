using System.Numerics;
using System.Configuration;
using BloodTourney;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LMWebAPI.Models;

public class Tournament
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    
    // Meta
    [BsonElement("creator_id")]
    public required string CreatorId { get; set; }
    
    [BsonElement("creation_datetime")]
    public required DateTime CreationDateTime { get; set; }
    
    [BsonElement("start_datetime")]
    public required DateTime StartDateTime { get; set; }
    
    
    // General Properties
    [BsonElement("tournament_name")]
    public required string TournamentName { get; set; }
    
    [BsonElement("team_rating_max")]
    public int TeamRatingMax { get; set; }
    
    [BsonElement("number_of_teams_max")]
    public int NumberOfTeamsMax { get; set; }

    [BsonElement("number_of_teams_min")]
    private readonly int _numberOfTeamsMin;
    public int NumberOfTeamsMin
    {
        get => _numberOfTeamsMin;
        init => _numberOfTeamsMin = value >= 2 ? value : throw new ArgumentException("Number of teams min must be 2 or higher.");
    }

    [BsonElement("registered_coach_ids")]
    public List<ObjectId> RegisteredCoachIds { get; set; }
    
    
    // Groups
    // [BsonElement("Groups")]
    // public CoachGroups CoachGroups { get; set; }
}