namespace Yenilen.Application.DTOs;

public class ServiceDto
{
    public string CategoryId { get; set; }
    public string ServiceId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Duration { get; set; }

    public string FormattedDuration
    {
        get
        {
            if (int.TryParse(Duration, out int mins))
            {
                int hours = mins / 60;
                int minutes = mins % 60;

                if (hours > 0 && minutes > 0)
                    return $"{hours} saat {minutes} dakika";
                if (hours > 0)
                    return $"{hours} saat";
                return $"{minutes} dakika";
            }

            return Duration;
        }
    }
    // public int MaxInSecond { get; set; } = 0;
    // public int MinInSecond { get; set; } = 0;
}