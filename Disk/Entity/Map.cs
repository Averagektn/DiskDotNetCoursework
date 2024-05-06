using Disk.Extension;

namespace Disk.Entity;

public partial class Map
{
    public long Id { get; set; }

    public string CoordinatesJson { get; set; } = null!;

    public string CreatedAt { get; set; } = null!;

    public long CreatedBy { get; set; }

    public string Name { get; set; } = null!;

    public virtual Doctor CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Session> Sessions { get; set; } = [];

    public override string ToString() => Name.ToUpperFirstLetter();
}
