namespace Disk.Entity;

public partial class Region
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<District> Districts { get; set; } = [];

    public override string ToString() => Name;
}
