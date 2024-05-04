namespace Disk.Entity;

public partial class DoctorCabinet
{
    public long Id { get; set; }

    public long Floor { get; set; }

    public long CabinetNum { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Operation> Operations { get; set; } = [];

    public virtual ICollection<Procedure> Procedures { get; set; } = [];
}
