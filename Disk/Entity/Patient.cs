namespace Disk.Entity;

public partial class Patient
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public long Address { get; set; }

    public string DateOfBirth { get; set; } = null!;

    public string? PhoneMobile { get; set; }

    public string? PhoneHome { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = [];

    public virtual ICollection<Card> Cards { get; set; } = [];

    public virtual ICollection<Note> Notes { get; set; } = [];

    public virtual Address AddressNavigation { get; set; } = null!;

    public virtual ICollection<Procedure> Procedures { get; set; } = [];
}
