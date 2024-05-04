namespace Disk.Entity;

public partial class PathToTarget
{
    public long Session { get; set; }

    public long Num { get; set; }

    public string CoordinatesJson { get; set; } = null!;

    public double Time { get; set; }

    public double AngleDistance { get; set; }

    public double AngleSpeed { get; set; }

    public double ApproachSpeed { get; set; }

    public virtual Session SessionNavigation { get; set; } = null!;
}
