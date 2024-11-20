using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public partial class HospitalContext : IdentityDbContext<User>
{
    public HospitalContext(DbContextOptions<HospitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }


    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientTreatment> PatientTreatments { get; set; }

    public virtual DbSet<Treatment> Treatments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     
        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("diagnoses_pkey");

            entity.ToTable("diagnoses");

            entity.HasIndex(e => e.DiseaseId, "IX_diagnoses_disease_id");

            entity.HasIndex(e => e.DoctorId, "IX_diagnoses_doctor_id");

            entity.HasIndex(e => e.PatientId, "IX_diagnoses_patient_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DiagnosisDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("diagnosis_date");
            entity.Property(e => e.DiseaseId).HasColumnName("disease_id");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");

            entity.HasOne(d => d.Disease).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.DiseaseId)
                .HasConstraintName("diagnoses_disease_id_fkey");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("diagnoses_doctor_id_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("diagnoses_patient_id_fkey");
        });

        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("diseases_pkey");

            entity.ToTable("diseases");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Severity).HasColumnName("severity");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("doctors_pkey");

            entity.ToTable("doctors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Specialty).HasColumnName("specialty");
            entity.Property(e => e.YearsExperience).HasColumnName("years_experience");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("patients_pkey");

            entity.ToTable("patients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<PatientTreatment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("patient_treatments_pkey");

            entity.ToTable("patient_treatments");

            entity.HasIndex(e => e.PatientId, "IX_patient_treatments_patient_id");

            entity.HasIndex(e => e.TreatmentId, "IX_patient_treatments_treatment_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("start_date");
            entity.Property(e => e.TreatmentId).HasColumnName("treatment_id");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientTreatments)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("patient_treatments_patient_id_fkey");

            entity.HasOne(d => d.Treatment).WithMany(p => p.PatientTreatments)
                .HasForeignKey(d => d.TreatmentId)
                .HasConstraintName("patient_treatments_treatment_id_fkey");
        });

        modelBuilder.Entity<Treatment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("treatments_pkey");

            entity.ToTable("treatments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        base.OnModelCreating(modelBuilder);
    }

}