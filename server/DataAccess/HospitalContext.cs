using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace DataAccess; public partial class HospitalContext : IdentityDbContext<IdentityUser>
{
    public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }
    public virtual DbSet<Diagnosis> Diagnoses { get; set; }
    public virtual DbSet<Disease> Diseases { get; set; }
    public virtual DbSet<Doctor> Doctors { get; set; }
    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<PatientTreatment> PatientTreatments { get; set; }
    public virtual DbSet<Treatment> Treatments { get; set; }
}
