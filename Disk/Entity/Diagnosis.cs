namespace Disk.Entity;

public partial class Diagnosis
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<M2mCardDiagnosis> M2mCardDiagnoses { get; set; } = [];
}
