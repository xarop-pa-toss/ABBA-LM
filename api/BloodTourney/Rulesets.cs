using System.Collections.Immutable;

namespace BloodTourney;

public class Ruleset
{
    public required IEnumerable<Tiers.TierParameters> Tiers { get; init; }
    public required VictoryPoints MatchVictoryPoints { get; init; }
    public required IEnumerable<string> TieBreakers { get; init; }
    public required TimeKeeping Timekeeping { get; init; }
    public required SkillStacking Skillstacking { get; init; }
    public required ImmutableArray<Inducement> Inducements { get; init; }
    public IEnumerable<string>? BannedStarPlayers { get; init; }
    public IEnumerable<string>? Guidelines { get; init; }
    public OtherRules AdditionalRules { get; init; }

    // Default constructor
    private Ruleset()
    {
        
    }

    public class Builder
    {
        private IEnumerable<Tiers.TierParameters>? _tiers;
        private VictoryPoints? _victoryPoints;
        private IEnumerable<string>? _tieBreakers;
        private TimeKeeping? _timekeeping;
        private SkillStacking? _skillstacking;
        private IEnumerable<Inducement>? _inducements;
        private IEnumerable<string>? _bannedStarPlayers;
        private IEnumerable<string>? _guidelines;
        private OtherRules? _otherRules;
        public Builder WithTiers(IEnumerable<Tiers.TierParameters> tiers)
        {
            this._tiers = tiers;
            return this;
        }

        public Builder WithVictoryPoints(VictoryPoints victoryPoints)
        {
            this._victoryPoints = victoryPoints;
            return this;
        }

        public Builder WithTieBreakers(IEnumerable<string> tieBreakers)
        {
            this._tieBreakers = tieBreakers;
            return this;
        }

        public Builder WithTimeKeeping(TimeKeeping timekeeping)
        {
            this._timekeeping = timekeeping;
            return this;
        }

        public Builder WithSkillStacking(SkillStacking skillStacking)
        {
            this._skillstacking = skillStacking;
            return this;
        }

        public Builder WithInducements(IEnumerable<Inducement> inducements)
        {
            this._inducements = inducements;
            return this;
        }

        public Builder WithBannedStarPlayers(IEnumerable<string> bannedStarPlayers)
        {
            this._bannedStarPlayers = bannedStarPlayers;
            return this;
        }

        public Builder WithGuidelines(IEnumerable<string> guidelines)
        {
            this._guidelines = guidelines;
            return this;
        }

        public Builder WithAdditionalRules(OtherRules otherRules)
        {
            this._otherRules = otherRules;
            return this;
        }

        public Ruleset Build()
        {
            // Validation
            if (_tiers == null || !_tiers.Any())
            {
                throw new InvalidOperationException("Tiers must not be null");
            }
            if (_victoryPoints == null)
            {
                throw new InvalidOperationException("MatchVictoryPoints must not be null");
            }
            if (_tieBreakers == null)
            {
                throw new InvalidOperationException("TieBreakers must not be null");
            }
            if (_timekeeping == null)
            {
                throw new InvalidOperationException("Timekeeping must not be null");
            }
            if (_skillstacking == null)
            {
                throw new InvalidOperationException("Skillstacking must not be null");
            }
            // Inducements, BannedStarPlayers, Guidelines and AdditionalRules can be null

            return new Ruleset
            {
                Tiers = _tiers,
                MatchVictoryPoints = _victoryPoints.Value,
                TieBreakers = _tieBreakers,
                Timekeeping = _timekeeping.Value,
                Skillstacking = _skillstacking.Value,
                Inducements = _inducements != null ? _inducements.ToImmutableArray() : ImmutableArray<Inducement>.Empty,
                BannedStarPlayers = _bannedStarPlayers,
                Guidelines = _guidelines,
                AdditionalRules = _otherRules ?? new OtherRules()
                {
                    UseGpForNonPlayerExtras = false,
                    AllowSameStarPlayerOnBothTeams = true,
                    RemovePlayersAddedDuringMatchAtGameEnd = true
                }
            };
        }
        public static Builder CreateBuilder() => new Builder();
    }
    
    
    #region STRUCTS/ENUMS
    public enum SkillStacks
    {
        NotAllowed,
        OnlyPrimaries,
        AllSkills
    }

    public struct SkillStacking()
    {
        public required SkillStacks StackingType { get; set; } = SkillStacks.OnlyPrimaries;

        /// <summary>
        /// Skill combinations that will consume extra slots from the alloted skill allowance (based on Tier)
        /// </summary>
        public Dictionary<ImmutableHashSet<string>, uint>? ExtraSkillstackCosts { get; init; }
        public required bool ExtraCostAppliesToDefaultSkills { get; init; }
    }

    public struct Inducement
    {
        public required uint Min { get; set; }
        public required uint Max { get; set; }
        public required string Name { get; set; }
        public required uint Cost { get; set; }

        /// <summary>
        /// Overrides the default cost of this inducement for a specific team. E.g., Master Chef for Halflings
        /// </summary>
        public Dictionary<string, uint>? CostOverrideForTeam { get; set; }

        /// <summary>
        /// Overrides default cost of this inducement for teams with a specific trait. Eg. Bribes for Bribery & Corruption teams
        /// </summary>
        public Dictionary<string, uint>? CostOverrideForTeamsWithTrait { get; set; }

        /// <summary>
        /// Team tiers allowed purchasing an inducement. All Tiers allowed if list is empty.
        /// </summary>
        public IEnumerable<int>? TiersAllowed { get; set; }
    }

    public struct TimeKeeping
    {
        public required uint MatchTimelimitInMinutes { get; set; }
        public required bool ChessClockAllowed { get; set; }
    }

    /// <summary>
    /// Default Win = 3, Draw = 1, Loss = 0
    /// </summary>
    public struct VictoryPoints()
    {
        public uint Win { get; set; } = 3;
        public uint Draw { get; set; } = 1;
        public uint Loss { get; set; } = 0;
    }

    public struct OtherRules()
    {
        /// <summary>
        /// Uses GP/TV for Rerolls, Assistant Coaches, Cheerleaders, Apothecary and Inducements
        /// </summary>
        public required bool UseGpForNonPlayerExtras = true;

        /// <summary>
        /// Players added to a roster during a match due to special rules are removed at the end of the match. Eg. Nurgle rotters
        /// </summary>
        public required bool RemovePlayersAddedDuringMatchAtGameEnd = true;

        /// <summary>
        /// If false, teams must have distinct Star Players.
        /// </summary>
        public required bool AllowSameStarPlayerOnBothTeams = true;

        /// <summary>
        /// Text only. Will not have mechanical effect.
        /// </summary>
        public IEnumerable<string>? AdditionalRules { get; set; }
    }
    #endregion
}