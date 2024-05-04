namespace Disk.Entity;

public partial class Card
{
    public long Id { get; set; }

    public long Patient { get; set; }

    public string Number { get; set; } = null!;

    public virtual ICollection<Contraindication> Contraindications { get; set; } = [];

    public virtual Patient PatientNavigation { get; set; } = null!;

    public virtual ICollection<M2mCardDiagnosis> M2mCardDiagnoses { get; set; } = [];

    public virtual ICollection<Operation> Operations { get; set; } = [];

    public virtual ICollection<Xray> Xrays { get; set; } = [];
}
