using BloodTourney.Models;
using LMWebAPI.Models.DTOs;
namespace LMWebAPI.Mappers;

public static class RulesetMapper
{
    public static Ruleset ToDomain(RulesetDTO dto)
    {
        return new Ruleset
        {
            Tiers = dto.Tiers,
            MatchVictoryPoints = dto.MatchVictoryPoints,
            TieBreakers = dto.TieBreakers,
            Timekeeping = dto.Timekeeping,
            Skillstacking = dto.Skillstacking,
            Inducements = dto.Inducements,
            BannedStarPlayers = dto.BannedStarPlayers,
            Guidelines = dto.Guidelines,
            AdditionalRules = dto.AdditionalRules
        };
    }

    public static RulesetDTO ToDto(Ruleset domain)
    {
        return new RulesetDTO
        {
            Tiers = domain.Tiers,
            MatchVictoryPoints = domain.MatchVictoryPoints,
            TieBreakers = domain.TieBreakers,
            Timekeeping = domain.Timekeeping,
            Skillstacking = domain.Skillstacking,
            Inducements = domain.Inducements,
            BannedStarPlayers = domain.BannedStarPlayers,
            Guidelines = domain.Guidelines,
            AdditionalRules = domain.AdditionalRules
        };
    }
}