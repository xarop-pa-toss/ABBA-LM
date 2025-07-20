using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
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

    public static string ParseToJsonString<T>(T input)
    {
        string serialized = JsonSerializer.Serialize(input);
        
        if (string.IsNullOrEmpty(serialized))
        {
            throw new ArgumentException($"Failed to serialize {typeof(T)} object to JSON string.");
        }
 
        return serialized;   
    }
    
    public readonly record struct ValidationResult()
    {
        private ImmutableList<string> Errors { get; init; } = ImmutableList<string>.Empty;
        private bool HasErrors => !Errors.IsEmpty;
        
        public void ThrowIfHasErrors(string message = "")
        {
            if (HasErrors)
                throw new ValidationException(message + "\n" + string.Join(Environment.NewLine, Errors));   
        }
        
        public static ValidationResult Valid() => new ValidationResult();
        public static ValidationResult Failure(IEnumerable<string> errors) => new ValidationResult { Errors = errors.ToImmutableList() };
    }

    /// <summary>
    /// Returns a ValidationResult with error if the collection is null or empty.
    /// </summary>
    /// <param name="collection"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns name="ValidationResult"></returns>
    public static ValidationResult CheckIfCollectionNullOrEmpty<T>(IEnumerable<T>? collection)
    {
        return collection == null || !collection.Any()
            ? ValidationResult.Failure(new List<string> { $"{nameof(collection)} collection object is null or empty." })
            : ValidationResult.Valid();
    }
}