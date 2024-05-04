namespace Disk.Entity;

public partial class Xray
{
    public long Id { get; set; }

    public string Date { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public string? Description { get; set; }

    public long Card { get; set; }

    public virtual Card CardNavigation { get; set; } = null!;
}
