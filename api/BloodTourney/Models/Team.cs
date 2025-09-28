using BloodTourney.Models.Enums;
using MongoDB.Bson;

namespace BloodTourney.Models;

public class Team
{
    public required ObjectId Id { get; init; }
    public required ObjectId UserId { get; init; }
    public required string Name { get; init; }
    public required TeamCodeNames TeamCodename { get; init; }

    private uint TournamentPoints;
}