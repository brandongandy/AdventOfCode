namespace AdventOfCode.Utility;

public static class MathExtensions
{
    public static bool IsBetween<T>(this T item, T min, T max, bool inclusive = true)
        where T : IComparable<T>
    {
        return inclusive 
            ? min.CompareTo(item) <= 0 && max.CompareTo(item) >= 0
            : min.CompareTo(item) < 0 && max.CompareTo(item) > 0;
    }
}
