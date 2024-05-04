namespace Disk.Entity;

public partial class Procedure
{
    public long Id { get; set; }

    public long AssignedBy { get; set; }

    public long AssignedTo { get; set; }

    public string DateTime { get; set; } = null!;

    public string Name { get; set; } = null!;

    public long? Cabinet { get; set; }

    public virtual Doctor AssignedByNavigation { get; set; } = null!;

    public virtual Patient AssignedToNavigation { get; set; } = null!;

    public virtual DoctorCabinet? CabinetNavigation { get; set; }
}
