namespace Disk.Entity;

public partial class Region
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = [];

    public virtual ICollection<District> Districts { get; set; } = [];
}
