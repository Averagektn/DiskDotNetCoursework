namespace Disk.Entity;

public partial class Appointment
{
    public long Id { get; set; }

    public string Time { get; set; } = null!;

    public long Doctor { get; set; }

    public long Patient { get; set; }

    public virtual Doctor DoctorNavigation { get; set; } = null!;

    public virtual Patient PatientNavigation { get; set; } = null!;

    public virtual ICollection<Session> Sessions { get; set; } = [];
}
