using System.Collections.Immutable;

namespace BloodTourney;

public partial class Core
{
    public enum Rulesets
    {
        BloodBowl2020,
        SardineBowl2025,
        Custom
    }

    public struct TeamTierParameters
    {
        public int TierNumber { get; set; }
        /// <summary>
        /// Maximum TV before skills and inducements are applied.
        /// </summary>
        public int Gps { get; set; }
        public int MaxPrimarySkills { get; set; }
        public int MaxSecondarySkills { get; set; }
        
        public int SkillStackingPlayerLimit { get; set; }
        public int MaxStarPlayers { get; set; }
    }

    internal ImmutableDictionary<ImmutableList<string>, TeamTierParameters> TeamTiers { get; } =
        ImmutableDictionary<ImmutableList<string>, TeamTierParameters>.Empty
        .Add(ImmutableList.Create("Amazon", "Dark Elf", "Dwarf", "Lizardmen", "Undead", "Underworld"),
            new TeamTierParameters()
            {
                TierNumber = 1,
                Gps = 1_150_000,
                MaxPrimarySkills = 6,
                MaxSecondarySkills = 0,
                SkillStackingPlayerLimit = 0,
                MaxStarPlayers = 0
            })
        .Add(ImmutableList.Create("Chaos Dwarf", "Necromantic", "Norse", "Orc", "Skaven", "Vampire", "Wood Elf"),
            new TeamTierParameters()
            {
                TierNumber = 2,
                Gps = 1_160_000,
                MaxPrimarySkills = 7,
                MaxSecondarySkills = 1,
                SkillStackingPlayerLimit = 0,
                MaxStarPlayers = 0
            })
        .Add(ImmutableList.Create("Elven Union", "Khorne", "High Elf", "Human", "Tomb King"),
            new TeamTierParameters()
            {
                TierNumber = 3,
                Gps = 1_170_000,
                MaxPrimarySkills = 8,
                MaxSecondarySkills = 1,
                SkillStackingPlayerLimit = 0,
                MaxStarPlayers = 1
            })
        .Add(ImmutableList.Create("Chaos Renegade", "Gnome", "Imperial Nobility", "Old World Alliance", "Slann"),
            new TeamTierParameters()
            {
                TierNumber = 4,
                Gps = 1_180_000,
                MaxPrimarySkills = 8,
                MaxSecondarySkills = 2,
                SkillStackingPlayerLimit = 2,
                MaxStarPlayers = 1
            })
        .Add(ImmutableList.Create("Black Orc", "Chaos", "Nurgle"),
            new TeamTierParameters()
            {
                TierNumber = 5,
                Gps = 1_200_000,
                MaxPrimarySkills = 9,
                MaxSecondarySkills = 2,
                SkillStackingPlayerLimit = 2,
                MaxStarPlayers = 1
            })
        .Add(ImmutableList.Create("Goblin", "Halfling", "Ogre", "Snotling"),
            new TeamTierParameters()
            {
                TierNumber = 6,
                Gps = 1_200_000,
                MaxPrimarySkills = 10,
                MaxSecondarySkills = 3,
                SkillStackingPlayerLimit = 4,
                MaxStarPlayers = 1
            });

}

    // TIER 4
    // 1.180k Gps
    // 8 skills
    //     max. 2 secondary
    //     may stack twice
    // Access 1 Star Player
    public static Rulesets GetPremadeRuleset(Rulesets ruleset)
    {
        switch (ruleset)
        {
            case Rulesets.BloodBowl2020:
                
            
        }
    }
}