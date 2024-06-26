﻿using Disk.Extension;

namespace Disk.Entity;

public partial class Doctor
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string? Patronymic { get; set; }

    public string Password { get; set; } = string.Empty;

    public virtual ICollection<Appointment> Appointments { get; set; } = [];

    public virtual ICollection<Map> Maps { get; set; } = [];

    public virtual ICollection<Note> Notes { get; set; } = [];

    public virtual ICollection<Operation> Operations { get; set; } = [];

    public virtual ICollection<Procedure> Procedures { get; set; } = [];

    public virtual ICollection<TargetFile> TargetFiles { get; set; } = [];

    public override string ToString() =>
        $"{Surname.ToUpperFirstLetter()} " +
        $"{Name.ToUpperFirstLetter()} " +
        $"{Patronymic?.ToUpperFirstLetter()}";
}
