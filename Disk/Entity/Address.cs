namespace Disk.Entity;

public partial class Address
{
    public long Id { get; set; }

    public long Region { get; set; }

    public string Street { get; set; } = null!;

    public long House { get; set; }

    public long Apartment { get; set; }

    public long Corpus { get; set; }

    public virtual Region RegionNavigation { get; set; } = null!;

    public virtual ICollection<Patient> Patients { get; set; } = [];
}
