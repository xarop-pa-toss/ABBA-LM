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
    public DbSet<Competition> Competitions { get; set; } = null!;
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
        
        modelBuilder.Entity<PlayerSkill>()
            .HasKey(ps => new
            {
                ps.PlayerId,
                ps.SkillId
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
        
        // One-to-One
        modelBuilder.Entity<Match>()
            .HasOne(m => m.MatchResults)
            .WithOne(mr => mr.Match)
            .HasForeignKey<MatchResult>(mr => mr.MatchId);

        // Let Postgres handle GUID creation for performance reasons
        // -> Gets all entity types in the model being build and checks of ID property
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var idProperty = entityType.FindProperty("Id");
        
            if (idProperty != null && idProperty.ClrType == typeof(Guid))
            {
                // Tells Postgres to create the GUID itself
                idProperty.SetDefaultValueSql("gen_random_uuid()");
            }
        }
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //TODO: Add CreateByUserId and UpdatedByUserId depending on current user context
        
        var changedEntries = ChangeTracker.Entries<BaseEntity>();
        foreach (var entry in changedEntries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedByUserId = Guid.NewGuid();
                // set CreatedByUserId from current user context
                entry.Entity.UpdatedByUserId = Guid.NewGuid();
                // set UpdatedByUserId 
            }
            else if (entry.State == EntityState.Modified)
            {}
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}