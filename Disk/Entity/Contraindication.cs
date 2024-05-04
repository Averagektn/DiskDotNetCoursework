namespace Disk.Entity;

public partial class Contraindication
{
    public long Id { get; set; }

    public long Card { get; set; }

    public string Name { get; set; } = null!;

    public virtual Card CardNavigation { get; set; } = null!;
}
