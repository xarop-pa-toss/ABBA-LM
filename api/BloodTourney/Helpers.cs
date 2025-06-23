using System.Collections.Immutable;
using System.Text.Json;

namespace BloodTourney;

public static class Helpers
{
    public static int GetNextPowerOfTwo(int value)
    {
        int powerOfTwo = 1;
        while (powerOfTwo < value)
        {
            powerOfTwo *= 2;
        }

        return powerOfTwo;
    }

    public static string ParseToJsonString (Tournament.Tournament tournament)
    { 
        return JsonSerializer.Serialize(tournament);
    }
    
    public readonly record struct ValidationResult()
    {
        public ImmutableList<string> Errors { get; init; } = ImmutableList<string>.Empty;
        
        public static ValidationResult Valid() => new ValidationResult();
        public static ValidationResult Failure(IEnumerable<string> errors) => new ValidationResult { Errors = errors.ToImmutableList() };
    }
}