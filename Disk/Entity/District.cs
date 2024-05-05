using Disk.Extension;

namespace Disk.Entity;

public partial class District
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long Region { get; set; }

    public virtual Region RegionNavigation { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = [];

    public override string ToString() => Name.ToUpperFirstLetter();
}
