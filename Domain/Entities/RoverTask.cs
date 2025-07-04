public class RoverTask
{
    public Guid Id { get; set; }
    public required string RoverName { get; set; }
    public TaskType TaskType { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    private DateTime _startsAt;
    public DateTime StartsAt
    {
        get => _startsAt;
        set
        {
            if (value.Kind != DateTimeKind.Utc)
                throw new ArgumentException("StartsAt debe estar en UTC.");
            _startsAt = value;
        }
    }
    public int DurationMinutes { get; set; }
    public Status Status { get; set; }

    public bool IsOverlapping(RoverTask other)
    {
        var endA = StartsAt.AddMinutes(DurationMinutes);
        var endB = other.StartsAt.AddMinutes(other.DurationMinutes);
        return StartsAt < endB && other.StartsAt < endA;
    }
}

public enum TaskType
{
    Drill,
    Sample,
    Photo,
    Charge
}

public enum Status
{
    Planned,
    InProgress,
    Completed,
    Aborted
}
