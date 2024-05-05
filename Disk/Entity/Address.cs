using Disk.Extension;

namespace Disk.Entity;

public partial class Address
{
    public long Id { get; set; }

    public long District { get; set; }

    public string Street { get; set; } = null!;

    public long House { get; set; }

    public long Apartment { get; set; }

    public long Corpus { get; set; }

    public virtual District DistrictNavigation { get; set; } = null!;

    public virtual ICollection<Patient> Patients { get; set; } = [];

    public override string ToString() => $"{Street.ToUpperFirstLetter()} {House}-{Apartment}({Corpus})";
}
