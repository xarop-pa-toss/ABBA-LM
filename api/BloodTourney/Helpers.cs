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
}