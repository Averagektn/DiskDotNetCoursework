namespace Disk.Entity;

public partial class SessionResult
{
    public long Id { get; set; }

    public double MathExp { get; set; }

    public double Deviation { get; set; }

    public double Dispersion { get; set; }

    public long Score { get; set; }

    public virtual Session Session { get; set; } = null!;
}
