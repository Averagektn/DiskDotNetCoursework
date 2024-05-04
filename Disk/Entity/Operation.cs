namespace Disk.Entity;

public partial class Operation
{
    public long Id { get; set; }

    public long Card { get; set; }

    public long? AsingnedBy { get; set; }

    public string Name { get; set; } = null!;

    public long? Cabinet { get; set; }

    public string DateTime { get; set; } = null!;

    public virtual Doctor? AsingnedByNavigation { get; set; }

    public virtual DoctorCabinet? CabinetNavigation { get; set; }

    public virtual Card CardNavigation { get; set; } = null!;
}
