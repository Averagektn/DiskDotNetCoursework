namespace Disk.Entity;

public partial class M2mCardDiagnosis
{
    public long Card { get; set; }

    public long Diagnosis { get; set; }

    public string DiagnosisStart { get; set; } = null!;

    public string? DiagnosisFinish { get; set; }

    public virtual Card CardNavigation { get; set; } = null!;

    public virtual Diagnosis DiagnosisNavigation { get; set; } = null!;
}
