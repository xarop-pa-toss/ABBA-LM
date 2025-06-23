using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Nodes;

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
    
    public static Ruleset GetPresetRuleset(RulesetPresets.RulesetPresetsEnum preset)
    {
        return preset switch
        {
            RulesetPresets.RulesetPresetsEnum.SardineBowl2025 => RulesetPresets.CreateSardineBowl2025Ruleset(),
            // RulesetPresets.RulesetPresetsEnum.EuroBowl2025 => CreateEuroBowl2025Ruleset(),
            _ => throw new ArgumentException($"Unknown preset: {preset}")
        };
    }
    
    public Byte[] EncryptRulesetIntoFile(Ruleset ruleset)
    {
        string json = JsonSerializer.Serialize(ruleset);
        return Encryption.EncryptStringToFile(json);
    }

    public Ruleset DecryptRulesetFile(byte[] encryptedRulesetFile)
    {
        string jsonDecrypted = Encryption.DecryptFromFileToString(encryptedRulesetFile);
        return JsonSerializer.Deserialize<Ruleset>(jsonDecrypted);
    }
    
    public class Builder
    {
        private readonly HashSet<string> _setProperties = new();
        
        private IEnumerable<Tiers.TierParameters>? _tiers;
        private VictoryPoints? _victoryPoints;
        private IEnumerable<string>? _tieBreakers;
        private TimeKeeping? _timekeeping;
        private SkillStacking? _skillstacking;
        private IEnumerable<Inducement>? _inducements;
        private IEnumerable<string>? _bannedStarPlayers;
        private IEnumerable<string>? _guidelines;
        private OtherRules? _otherRules;
        
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
        
        public Builder WithTiers(IEnumerable<Tiers.TierParameters> tiers)
        {
            _tiers = tiers;
            _setProperties.Add(nameof(Tiers));
            return this;
        }

        public Builder WithVictoryPoints(VictoryPoints victoryPoints)
        {
            _victoryPoints = victoryPoints;
            _setProperties.Add(nameof(VictoryPoints));
            return this;
        }

        public Builder WithTieBreakers(IEnumerable<string> tieBreakers)
        {
            _tieBreakers = tieBreakers;
            _setProperties.Add("TieBreakers");
            return this;
        }

        public Builder WithTimeKeeping(TimeKeeping timekeeping)
        {
            _timekeeping = timekeeping;
            _setProperties.Add(nameof(TimeKeeping));
            return this;
        }

        public Builder WithSkillStacking(SkillStacking skillStacking)
        {
            _skillstacking = skillStacking;
            _setProperties.Add(nameof(SkillStacking));
            return this;
        }

        public Builder WithInducements(IEnumerable<Inducement> inducements)
        {
            _inducements = inducements;
            _setProperties.Add("Inducements");
            return this;
        }

        public Builder WithBannedStarPlayers(IEnumerable<string> bannedStarPlayers)
        {
            _bannedStarPlayers = bannedStarPlayers;
            _setProperties.Add("BannedStarPlayers");
            return this;
        }

        public Builder WithGuidelines(IEnumerable<string> guidelines)
        {
            _guidelines = guidelines;
            _setProperties.Add("Guidelines");
            return this;
        }

        public Builder WithAdditionalRules(OtherRules otherRules)
        {
            _otherRules = otherRules;
            _setProperties.Add(nameof(OtherRules));
            return this;
        }
        
        public static Builder Create() => new Builder();
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
        /// Skill combinations that will consume extra slots from the allotted skill allowance (based on Tier)
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
        /// Overrides default cost of this inducement for teams with a specific trait. E.g. Bribes for Bribery & Corruption teams
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
        /// Players added to a roster during a match due to special rules are removed at the end of the match. E.g. Nurgle rotters
        /// </summary>
        public required bool RemovePlayersAddedDuringMatchAtGameEnd = true;

        /// <summary>
        /// If false, teams must have distinct Star Players.
        /// </summary>
        public required bool AllowSameStarPlayerOnBothTeams = true;

        /// <summary>
        /// Text only. Will not have a mechanical effect.
        /// </summary>
        public IEnumerable<string>? AdditionalRules { get; set; }
    }
    #endregion
}