namespace Disk.Entity;

public partial class TargetFile
{
    public long Id { get; set; }

    public string Filepath { get; set; } = null!;

    public long AddedBy { get; set; }

    public virtual Doctor AddedByNavigation { get; set; } = null!;
}
