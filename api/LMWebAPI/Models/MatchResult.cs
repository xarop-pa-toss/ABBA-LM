using LMWebAPI.Data;
using LMWebAPI.Models.Enums;
namespace LMWebAPI.Models;

public class MatchResult : BaseEntity
{
    public Guid MatchId { get; set; }
    public Match Match { get; set; } = null!;

    // Stats
    public int TouchdownsHome { get; set; } = 0;
    public int CasualtiesHome { get; set; } = 0;
    public int CompletionsHome { get; set; } = 0;
    public int InterceptionsHome { get; set; } = 0;
    public int FoulsHome { get; set; } = 0;

    public int TouchdownsAway { get; set; } = 0;
    public int CasualtiesAway { get; set; } = 0;
    public int CompletionsAway { get; set; } = 0;
    public int InterceptionsAway { get; set; } = 0;
    public int FoulsAway { get; set; } = 0;

    public ForfeitReasons? Forfeit { get; set; }
}