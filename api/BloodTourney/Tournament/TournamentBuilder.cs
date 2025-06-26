using BloodTourney.Models;

namespace BloodTourney.Tournament;

public interface ITournamentBuilder
{
    ITournamentBuilder WithOrganizerId(string organizerId);
    ITournamentBuilder WithTournamentName(string tournamentName);
    ITournamentBuilder WithPlayerLimit(int playerLimit);
    ITournamentBuilder WithStartDate(DateOnly startDate);
    ITournamentBuilder WithStartTime(TimeOnly startTime);
    ITournamentBuilder WithEndDate(DateOnly endDate);
    ITournamentBuilder WithEndTime(TimeOnly endTime);
    ITournamentBuilder WithLocation(string location);
    ITournamentBuilder WithConfiguration(Models.TournamentConfig config);
    Models.Tournament Build();
}

public class TournamentBuilder : ITournamentBuilder 
{
    private string _organizerId;
    private string _tournamentName;
    private int _playerLimit;
    private DateOnly _startDate;
    private TimeOnly _startTime;
    private DateOnly _endDate;
    private TimeOnly _endTime;
    private string _location;
    private TournamentConfig _configuration;

    public static ITournamentBuilder Create() => new TournamentBuilder();
    
    public ITournamentBuilder WithOrganizerId(string organizerId)
    {
        var str = organizerId.Trim();
        if (string.IsNullOrEmpty(str))
            throw new ArgumentException("Organizer ID cannot be null or empty.");       
        
        _organizerId = str;
        return this;
    }
    
    public ITournamentBuilder WithTournamentName(string tournamentName)
    {
        var str = tournamentName.Trim();
        if (string.IsNullOrEmpty(str))
            throw new ArgumentException("Tournament Name cannot be null or empty.");       
        
        _tournamentName = str;
        return this;
    }
    
    public ITournamentBuilder WithPlayerLimit(int playerLimit)
    {
        if (playerLimit < 0)
            throw new ArgumentException("Player limit must be greater than 0.");
        
        _playerLimit = playerLimit;
        return this;       
    }
    
    public ITournamentBuilder WithStartDate(DateOnly startDate)
    {
        if (startDate.CompareTo(DateTime.Today.Date) < 0)
            throw new ArgumentException("Start date must be in the future.");
        
        _startDate = startDate;
        return this;       
    }
    public ITournamentBuilder WithStartTime(TimeOnly startTime)
    {
        _startTime = startTime;
        return this;       
    }
    public ITournamentBuilder WithEndDate(DateOnly endDate)
    {
        if (endDate.CompareTo(DateTime.Today.Date) < 0)
            throw new ArgumentException("Start date must be in the future.");
        
        _endDate = endDate;
        return this;
    }
    public ITournamentBuilder WithEndTime(TimeOnly endTime)
    {
        _endTime = endTime;
        return this;    
    }
    public ITournamentBuilder WithLocation(string location)
    {
        _location = location;
        return this;
    }
    public ITournamentBuilder WithConfiguration(TournamentConfig config)
    {
        _configuration = config ?? throw new ArgumentException("Configuration cannot be null.");
        return this;       
    }
    public Models.Tournament Build()
    {
        throw new NotImplementedException();
    }
}