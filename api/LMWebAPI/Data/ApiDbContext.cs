using LMWebAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace LMWebAPI.Data;

public class ApiDbContext : DbContext
{
    //TODO: Add Indexes to Soft Delete fields like 'WHERE IsDeleted = False', FKs and commonly queried fields like Coach.Username or Coach.Email
    //TODO: Add Fluent Validation for email formats, username uniqueness, positive number constraints for stats, etc 
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
    public DbSet<Skill> Skills { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Tournament> Tournaments { get; set; } = null!;
    public DbSet<TournamentRound> TournamentRounds { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region BaseEntity and BaseJunctionEntity config
        modelBuilder.Ignore<BaseEntity>();
        modelBuilder.Ignore<BaseJunctionEntity>();
        // modelBuilder.Entity<BaseEntity>()
        //     .Property(e => e.Id)
        //     .HasDefaultValueSql("gen_random_uuid()")
        //     .ValueGeneratedOnAdd();
        // modelBuilder.Entity<BaseEntity>()
        //     .Property(e => e.CreatedAt)
        //     .HasDefaultValueSql("NOW()")
        //     .ValueGeneratedOnAdd();
        // modelBuilder.Entity<BaseEntity>()
        //     .Property(e => e.UpdatedAt)
        //     .HasDefaultValueSql("NOW()");
        //
        // modelBuilder.Entity<BaseJunctionEntity>()
        //     .Property(e => e.CreatedAt)
        //     .HasDefaultValueSql("NOW()");
        #endregion
        
        #region Team-Match Relationships
        modelBuilder.Entity<Match>()
            .HasOne(m => m.HomeTeam)
            .WithMany(t => t.HomeMatches)
            .HasForeignKey(m => m.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete issues

        modelBuilder.Entity<Match>()
            .HasOne(m => m.AwayTeam)
            .WithMany(t => t.AwayMatches)
            .HasForeignKey(m => m.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete issues
        #endregion
        
        #region Competition hierarchy config
        modelBuilder.Entity<Competition>()
            .HasDiscriminator<string>("CompetitionType")
            .HasValue<League>("League")
            .HasValue<Tournament>("Tournament");
        #endregion
        
        #region Composite Keys
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
        #endregion

        #region One-to-One
        modelBuilder.Entity<Match>()
            .HasOne(m => m.MatchResults)
            .WithOne(mr => mr.Match)
            .HasForeignKey<MatchResult>(mr => mr.MatchId);
        #endregion
        
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    private void UpdateTimestamps()
    {
        //TODO: Add CreateByUserId and UpdatedByUserId depending on current user context
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity baseEntity)
            {
                baseEntity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}