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
}