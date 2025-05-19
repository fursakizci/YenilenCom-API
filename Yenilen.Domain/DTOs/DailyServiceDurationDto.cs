namespace Yenilen.Domain.DTOs;

public class DailyServiceDurationDto
{
    public int StaffId { get; set; }
    public DateTime Date { get; set; }
    public double TotalDurationMinutes { get; set; }
    public List<TimeRange> Appointments { get; set; }
}


public sealed class TimeRange
{
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }

    public TimeRange(TimeSpan start, TimeSpan end)
    {
        // if (end <= start)
        //     throw new ArgumentException("End time must be after start time");

        Start = start;
        End = end;
    }

    public bool Overlaps(TimeSpan otherStart, TimeSpan otherEnd)
    {
        return Start < otherEnd && End > otherStart;
    }

    public bool Overlaps(TimeRange other)
    {
        return Overlaps(other.Start, other.End);
    }
}