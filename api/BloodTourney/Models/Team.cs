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

public enum TeamCodeNames
{
    Amazons,
    BlackOrcs,
    Chaos,
    ChaosDwarves,
    ChaosRenegades,
    DarkElves,
    Dwarves,
    ElvenUnion,
    Gnomes,
    Goblins,
    Halflings,
    HighElves,
    Humans,
    ImperialNobility,
    Khorne,
    Lizardmen,
    Necromantic,
    Norse,
    Nurgle,
    Ogres,
    OldWorldAlliance,
    Orcs,
    Skaven,
    Slann,
    Snotlings,
    TombKings,
    Undead,
    Underworld,
    Vampires,
    WoodElves
}