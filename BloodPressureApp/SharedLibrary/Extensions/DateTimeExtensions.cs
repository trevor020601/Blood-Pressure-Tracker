namespace SharedLibrary.Extensions;

public interface IDateTimeRange
{
    DateTime Start { get; }
    DateTime End { get; }
}

public readonly record struct DateTimeRange : IDateTimeRange
{
    public DateTime Start { get; }

    public DateTime End { get; }

    public DateTimeRange(DateTime start, DateTime end)
    {
        if (start > end)
        {
            throw new ArgumentException("Start date must occur before end date.");
        }

        Start = start;
        End = end;
    }
}

public static class DateTimeExtensions
{
    /// <summary>
    /// Calculates the age in years based on a range of dates
    /// </summary>
    /// <param name="range"></param>
    /// <returns>Age in years.</returns>
    public static int CalculateAge(DateTimeRange range)
    {
        var age = range.End.Year - range.Start.Year;
        // Adjust age if birthday has yet to occur
        if (range.End.Month < range.Start.Month ||
            range.End.Month == range.Start.Month &&
            range.End.Day < range.Start.Day)
        {
            age--;
        }
        return age;
    }
}
