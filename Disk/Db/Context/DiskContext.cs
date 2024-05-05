using Disk.Entity;
using Microsoft.EntityFrameworkCore;

namespace Disk.Db.Context;

public partial class DiskContext : DbContext
{
    public DiskContext()
    {
        Database.EnsureCreated();
    }

    public DiskContext(DbContextOptions<DiskContext> options) : base(options) { }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<CardDiagnosis> CardDiagnoses { get; set; }

    public virtual DbSet<Contraindication> Contraindications { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorAppointment> DoctorAppointments { get; set; }

    public virtual DbSet<DoctorCabinet> DoctorCabinets { get; set; }

    public virtual DbSet<M2mCardDiagnosis> M2mCardDiagnoses { get; set; }

    public virtual DbSet<Map> Maps { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<PathInTarget> PathInTargets { get; set; }

    public virtual DbSet<PathToTarget> PathToTargets { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientCard> PatientCards { get; set; }

    public virtual DbSet<Procedure> Procedures { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<SessionResult> SessionResults { get; set; }

    public virtual DbSet<TargetFile> TargetFiles { get; set; }

    public virtual DbSet<Xray> Xrays { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source = C:\\Archive\\6 semester\\DiskDotNetCoursework\\Disk\\Db\\disk.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("address");

            entity.Property(e => e.Id).HasColumnName("addr_id");
            entity.Property(e => e.Apartment).HasColumnName("addr_apartment");
            entity.Property(e => e.Corpus)
                .HasDefaultValue(1)
                .HasColumnName("addr_corpus");
            entity.Property(e => e.House).HasColumnName("addr_house");
            entity.Property(e => e.District).HasColumnName("addr_district");
            entity.Property(e => e.Street)
                .UseCollation("RTRIM")
                .HasColumnName("addr_street");

            entity.HasOne(d => d.DistrictNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.District)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("appointment");

            entity.HasIndex(e => e.Time, "IDX_time").IsDescending();

            entity.Property(e => e.Id).HasColumnName("app_id");
            entity.Property(e => e.Doctor).HasColumnName("app_doctor");
            entity.Property(e => e.Patient).HasColumnName("app_patient");
            entity.Property(e => e.Time)
                .UseCollation("RTRIM")
                .HasColumnName("app_time");

            entity.HasOne(d => d.DoctorNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.Doctor)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.PatientNavigation).WithMany(p => p.Appointments).HasForeignKey(d => d.Patient);
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("card");

            entity.HasIndex(e => e.Number, "IDX_number");

            entity.HasIndex(e => e.Number, "UNQ_IDX_number").IsUnique();

            entity.Property(e => e.Id).HasColumnName("crd_id");
            entity.Property(e => e.Number)
                .UseCollation("RTRIM")
                .HasColumnType("TEXT (9)")
                .HasColumnName("crd_number");
            entity.Property(e => e.Patient).HasColumnName("crd_patient");

            entity.HasOne(d => d.PatientNavigation).WithMany(p => p.Cards).HasForeignKey(d => d.Patient);
        });

        modelBuilder.Entity<CardDiagnosis>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("card_diagnosis");

            entity.Property(e => e.C2dDiagnosisFinish).HasColumnName("c2d_diagnosis_finish");
            entity.Property(e => e.C2dDiagnosisStart).HasColumnName("c2d_diagnosis_start");
            entity.Property(e => e.CrdId).HasColumnName("crd_id");
            entity.Property(e => e.CrdNumber)
                .HasColumnType("TEXT (9)")
                .HasColumnName("crd_number");
            entity.Property(e => e.DiaId).HasColumnName("dia_id");
            entity.Property(e => e.DiaName).HasColumnName("dia_name");
        });

        modelBuilder.Entity<Contraindication>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("contraindication");

            entity.Property(e => e.Id).HasColumnName("con_id");
            entity.Property(e => e.Card).HasColumnName("con_card");
            entity.Property(e => e.Name)
                .UseCollation("RTRIM")
                .HasColumnName("con_name");

            entity.HasOne(d => d.CardNavigation).WithMany(p => p.Contraindications).HasForeignKey(d => d.Card);
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("diagnosis");

            entity.HasIndex(e => e.Name, "IX_diagnosis_dia_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("dia_id");
            entity.Property(e => e.Name)
                .UseCollation("RTRIM")
                .HasColumnName("dia_name");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("district");

            entity.HasIndex(e => e.Name, "IX_district_dst_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("dst_id");
            entity.Property(e => e.Name)
                .UseCollation("RTRIM")
                .HasColumnName("dst_name");
            entity.Property(e => e.Region).HasColumnName("dst_region");

            entity.HasOne(d => d.RegionNavigation).WithMany(p => p.Districts)
                .HasForeignKey(d => d.Region)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("doctor");

            entity.Property(e => e.Id).HasColumnName("doc_id");
            entity.Property(e => e.Name)
                .UseCollation("RTRIM")
                .HasColumnName("doc_name");
            entity.Property(e => e.Password)
                .UseCollation("RTRIM")
                .HasColumnName("doc_password");
            entity.Property(e => e.Patronymic)
                .UseCollation("RTRIM")
                .HasColumnName("doc_patronymic");
            entity.Property(e => e.Surname)
                .UseCollation("RTRIM")
                .HasColumnName("doc_surname");
        });

        modelBuilder.Entity<DoctorAppointment>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("doctor_appointment");

            entity.Property(e => e.AppId).HasColumnName("app_id");
            entity.Property(e => e.AppTime).HasColumnName("app_time");
            entity.Property(e => e.DocId).HasColumnName("doc_id");
            entity.Property(e => e.DocName).HasColumnName("doc_name");
            entity.Property(e => e.DocPatronymic).HasColumnName("doc_patronymic");
            entity.Property(e => e.DocSurname).HasColumnName("doc_surname");
            entity.Property(e => e.PatId).HasColumnName("pat_id");
            entity.Property(e => e.PatName)
                .HasColumnType("TEXT (20)")
                .HasColumnName("pat_name");
            entity.Property(e => e.PatPatronymic)
                .HasColumnType("TEXT (30)")
                .HasColumnName("pat_patronymic");
            entity.Property(e => e.PatSurname)
                .HasColumnType("TEXT (30)")
                .HasColumnName("pat_surname");
        });

        modelBuilder.Entity<DoctorCabinet>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("doctor_cabinet");

            entity.HasIndex(e => new { e.Floor, e.CabinetNum }, "IX_doctor_cabinet_dc_floor_dc_cabinet_num").IsUnique();

            entity.Property(e => e.Id).HasColumnName("dc_id");
            entity.Property(e => e.CabinetNum).HasColumnName("dc_cabinet_num");
            entity.Property(e => e.Floor).HasColumnName("dc_floor");
            entity.Property(e => e.Name)
                .UseCollation("RTRIM")
                .HasColumnName("dc_name");
        });

        modelBuilder.Entity<M2mCardDiagnosis>(entity =>
        {
            entity.HasKey(e => new { e.Card, e.Diagnosis });

            entity.ToTable("m2m_card_diagnosis");

            entity.Property(e => e.Card).HasColumnName("c2d_card");
            entity.Property(e => e.Diagnosis).HasColumnName("c2d_diagnosis");
            entity.Property(e => e.DiagnosisFinish)
                .UseCollation("RTRIM")
                .HasColumnName("c2d_diagnosis_finish");
            entity.Property(e => e.DiagnosisStart)
                .UseCollation("RTRIM")
                .HasColumnName("c2d_diagnosis_start");

            entity.HasOne(d => d.CardNavigation).WithMany(p => p.M2mCardDiagnoses).HasForeignKey(d => d.Card);

            entity.HasOne(d => d.DiagnosisNavigation).WithMany(p => p.M2mCardDiagnoses)
                .HasForeignKey(d => d.Diagnosis)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Map>(entity =>
        {
            entity.ToTable("map");

            entity.HasIndex(e => e.Name, "IX_map_map_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("map_id");
            entity.Property(e => e.CoordinatesJson).HasColumnName("map_coordinates_json");
            entity.Property(e => e.CreatedAt)
                .UseCollation("RTRIM")
                .HasColumnName("map_created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("map_created_by");
            entity.Property(e => e.Name)
                .UseCollation("RTRIM")
                .HasColumnName("map_name");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Maps)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("note");

            entity.Property(e => e.Id).HasColumnName("nt_id");
            entity.Property(e => e.Doctor).HasColumnName("nt_doctor");
            entity.Property(e => e.Patient).HasColumnName("nt_patient");
            entity.Property(e => e.Text).HasColumnName("nt_text");

            entity.HasOne(d => d.DoctorNavigation).WithMany(p => p.Notes)
                .HasForeignKey(d => d.Doctor)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.PatientNavigation).WithMany(p => p.Notes).HasForeignKey(d => d.Patient);
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("operation");

            entity.Property(e => e.Id).HasColumnName("op_id");
            entity.Property(e => e.AsingnedBy).HasColumnName("op_asingned_by");
            entity.Property(e => e.Cabinet).HasColumnName("op_cabinet");
            entity.Property(e => e.Card).HasColumnName("op_card");
            entity.Property(e => e.DateTime)
                .UseCollation("NOCASE")
                .HasColumnName("op_date_time");
            entity.Property(e => e.Name)
                .UseCollation("RTRIM")
                .HasColumnName("op_name");

            entity.HasOne(d => d.AsingnedByNavigation).WithMany(p => p.Operations)
                .HasForeignKey(d => d.AsingnedBy)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.CabinetNavigation).WithMany(p => p.Operations)
                .HasForeignKey(d => d.Cabinet)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.CardNavigation).WithMany(p => p.Operations).HasForeignKey(d => d.Card);
        });

        modelBuilder.Entity<PathInTarget>(entity =>
        {
            entity.HasKey(e => new { e.Session, e.TargetId });

            entity.ToTable("path_in_target");

            entity.Property(e => e.Session).HasColumnName("pit_session");
            entity.Property(e => e.TargetId).HasColumnName("pit_target_id");
            entity.Property(e => e.CoordinatesJson).HasColumnName("pit_coordinates_json");

            entity.HasOne(d => d.SessionNavigation).WithMany(p => p.PathInTargets).HasForeignKey(d => d.Session);
        });

        modelBuilder.Entity<PathToTarget>(entity =>
        {
            entity.HasKey(e => new { e.Session, e.Num });

            entity.ToTable("path_to_target");

            entity.Property(e => e.Session).HasColumnName("ptt_session");
            entity.Property(e => e.Num).HasColumnName("ptt_num");
            entity.Property(e => e.AngleDistance).HasColumnName("ptt_angle_distance");
            entity.Property(e => e.AngleSpeed).HasColumnName("ptt_angle_speed");
            entity.Property(e => e.ApproachSpeed).HasColumnName("ptt_approach_speed");
            entity.Property(e => e.CoordinatesJson).HasColumnName("ptt_coordinates_json");
            entity.Property(e => e.Time).HasColumnName("ptt_time");

            entity.HasOne(d => d.SessionNavigation).WithMany(p => p.PathToTargets).HasForeignKey(d => d.Session);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("patient");

            entity.HasIndex(e => new { e.Name, e.Surname, e.Patronymic }, "IDX_name");

            entity.Property(e => e.Id).HasColumnName("pat_id");
            entity.Property(e => e.Address).HasColumnName("pat_address");
            entity.Property(e => e.DateOfBirth)
                .UseCollation("NOCASE")
                .HasColumnName("pat_date_of_birth");
            entity.Property(e => e.Name)
                .UseCollation("NOCASE")
                .HasColumnType("TEXT (20)")
                .HasColumnName("pat_name");
            entity.Property(e => e.Patronymic)
                .UseCollation("NOCASE")
                .HasColumnType("TEXT (30)")
                .HasColumnName("pat_patronymic");
            entity.Property(e => e.PhoneHome)
                .UseCollation("NOCASE")
                .HasColumnName("pat_phone_home");
            entity.Property(e => e.PhoneMobile)
                .UseCollation("NOCASE")
                .HasColumnName("pat_phone_mobile");
            entity.Property(e => e.Surname)
                .UseCollation("NOCASE")
                .HasColumnType("TEXT (30)")
                .HasColumnName("pat_surname");

            entity.HasOne(d => d.AddressNavigation).WithMany(p => p.Patients).HasForeignKey(d => d.Address);
        });

        modelBuilder.Entity<PatientCard>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("patient_card");

            entity.Property(e => e.CrdId).HasColumnName("crd_id");
            entity.Property(e => e.CrdNumber)
                .HasColumnType("TEXT (9)")
                .HasColumnName("crd_number");
            entity.Property(e => e.PatId).HasColumnName("pat_id");
            entity.Property(e => e.PatName)
                .HasColumnType("TEXT (20)")
                .HasColumnName("pat_name");
            entity.Property(e => e.PatPatronymic)
                .HasColumnType("TEXT (30)")
                .HasColumnName("pat_patronymic");
            entity.Property(e => e.PatSurname)
                .HasColumnType("TEXT (30)")
                .HasColumnName("pat_surname");
        });

        modelBuilder.Entity<Procedure>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("procedure");

            entity.Property(e => e.Id).HasColumnName("pro_id");
            entity.Property(e => e.AssignedBy).HasColumnName("pro_assigned_by");
            entity.Property(e => e.AssignedTo).HasColumnName("pro_assigned_to");
            entity.Property(e => e.Cabinet).HasColumnName("pro_cabinet");
            entity.Property(e => e.DateTime)
                .UseCollation("NOCASE")
                .HasColumnName("pro_date_time");
            entity.Property(e => e.Name)
                .UseCollation("NOCASE")
                .HasColumnName("pro_name");

            entity.HasOne(d => d.AssignedByNavigation).WithMany(p => p.Procedures)
                .HasForeignKey(d => d.AssignedBy)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.Procedures).HasForeignKey(d => d.AssignedTo);

            entity.HasOne(d => d.CabinetNavigation).WithMany(p => p.Procedures)
                .HasForeignKey(d => d.Cabinet)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("region");

            entity.HasIndex(e => e.Name, "IX_region_rgn_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("rgn_id");
            entity.Property(e => e.Name)
                .UseCollation("NOCASE")
                .HasColumnName("rgn_name");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("report");

            entity.Property(e => e.AppTime).HasColumnName("app_time");
            entity.Property(e => e.DocName).HasColumnName("doc_name");
            entity.Property(e => e.DocPatronymic).HasColumnName("doc_patronymic");
            entity.Property(e => e.DocSurname).HasColumnName("doc_surname");
            entity.Property(e => e.MapName).HasColumnName("map_name");
            entity.Property(e => e.PatName)
                .HasColumnType("TEXT (20)")
                .HasColumnName("pat_name");
            entity.Property(e => e.PatPatronymic)
                .HasColumnType("TEXT (30)")
                .HasColumnName("pat_patronymic");
            entity.Property(e => e.PatSurname)
                .HasColumnType("TEXT (30)")
                .HasColumnName("pat_surname");
            entity.Property(e => e.SesDate).HasColumnName("ses_date");
            entity.Property(e => e.SresDeviation).HasColumnName("sres_deviation");
            entity.Property(e => e.SresDispersion).HasColumnName("sres_dispersion");
            entity.Property(e => e.SresId).HasColumnName("sres_id");
            entity.Property(e => e.SresMathExp).HasColumnName("sres_math_exp");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("session");

            entity.HasIndex(e => e.LogFilePath, "IX_session_ses_log_file_path").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ses_id");
            entity.Property(e => e.Appointment).HasColumnName("ses_appointment");
            entity.Property(e => e.Date)
                .UseCollation("NOCASE")
                .HasColumnName("ses_date");
            entity.Property(e => e.LogFilePath)
                .UseCollation("RTRIM")
                .HasColumnName("ses_log_file_path");
            entity.Property(e => e.Map).HasColumnName("ses_map");

            entity.HasOne(d => d.AppointmentNavigation).WithMany(p => p.Sessions).HasForeignKey(d => d.Appointment);

            entity.HasOne(d => d.MapNavigation).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.Map)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<SessionResult>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("session_result");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("sres_id");
            entity.Property(e => e.Deviation).HasColumnName("sres_deviation");
            entity.Property(e => e.Dispersion).HasColumnName("sres_dispersion");
            entity.Property(e => e.MathExp).HasColumnName("sres_math_exp");
            entity.Property(e => e.Score).HasColumnName("sres_score");

            entity.HasOne(d => d.Session).WithOne(p => p.SessionResult).HasForeignKey<SessionResult>(d => d.Id);
        });

        modelBuilder.Entity<TargetFile>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("target_file");

            entity.HasIndex(e => e.Filepath, "IX_target_file_tf_filepath").IsUnique();

            entity.Property(e => e.Id).HasColumnName("tf_id");
            entity.Property(e => e.AddedBy).HasColumnName("tf_added_by");
            entity.Property(e => e.Filepath)
                .UseCollation("RTRIM")
                .HasColumnName("tf_filepath");

            entity.HasOne(d => d.AddedByNavigation).WithMany(p => p.TargetFiles)
                .HasForeignKey(d => d.AddedBy)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Xray>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("xray");

            entity.HasIndex(e => e.FilePath, "IX_xray_xr_file_path").IsUnique();

            entity.Property(e => e.Id).HasColumnName("xr_id");
            entity.Property(e => e.Card).HasColumnName("xr_card");
            entity.Property(e => e.Date)
                .UseCollation("NOCASE")
                .HasColumnName("xr_date");
            entity.Property(e => e.Description).HasColumnName("xr_description");
            entity.Property(e => e.FilePath)
                .UseCollation("RTRIM")
                .HasColumnName("xr_file_path");

            entity.HasOne(d => d.CardNavigation).WithMany(p => p.Xrays).HasForeignKey(d => d.Card);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
