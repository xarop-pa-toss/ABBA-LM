using LMWebAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace LMWebAPI.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }

    // DbSets are which Models should be turned to SQL tables
    public DbSet<Coach> Coaches { get; set; } = null!;
    public DbSet<Injury> Injuries { get; set; } = null!;
    public DbSet<League> Leagues { get; set; } = null!;
    public DbSet<LeagueRound> LeagueRounds { get; set; } = null!;
    public DbSet<Match> Matches { get; set; } = null!;
    public DbSet<MatchResult> MatchResults { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<PlayerInjury> PlayerInjuries { get; set; } = null!;
    public DbSet<PlayerSkill> PlayerSkills { get; set; } = null!;
    public DbSet<Positional> Positionals { get; set; } = null!;
    public DbSet<PositionalRoster> PositionalRosters { get; set; } = null!;
    public DbSet<PositionalSkill> PositionalSkills { get; set; } = null!;
    public DbSet<Roster> Rosters { get; set; } = null!;
    public DbSet<Round> Rounds { get; set; } = null!;
    public DbSet<Skill> Skills { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Tournament> Tournaments { get; set; } = null!;
    public DbSet<TournamentRound> TournamentRounds { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Composite keys
        modelBuilder.Entity<PlayerInjury>()
            .HasKey(pi => new
            {
                pi.PlayerId,
                pi.InjuryId
            });

        modelBuilder.Entity<PositionalRoster>()
            .HasKey(pr => new
            {
                pr.PositionalId,
                pr.RosterId
            });

        modelBuilder.Entity<PositionalSkill>()
            .HasKey(ps => new
            {
                ps.PositionalId,
                ps.SkillId
            });


        base.OnModelCreating(modelBuilder);
    }
}