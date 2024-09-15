using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public class HospitalRepository(HospitalContext context) : IRepository
{
    public List<Doctor> GetAllDoctors()
    {
        return context.Doctors.ToList();
    }

    public List<Doctor> GetAllDoctorsIncludingDiagnoses()
    {
        return context.Doctors.Include(d => d.Diagnoses).ToList();
    }


    public Doctor GetDoctorWhoMadeMostDiagnoses()
    {
        return context.Doctors.OrderByDescending(d => d.Diagnoses.Count).First();
    }

    public List<Patient> GetAllPatientsWhoHasHadTreatmentWithId(int treatmentId)
    {
        return context.Patients.Where(p => p.PatientTreatments.Any(t => t.TreatmentId == treatmentId)).ToList();
    }

    public List<Doctor> GetAllDoctorsWithSpecialty(string specialty)
    {
        return context.Doctors.Where(d => d.Specialty.Equals(specialty)).ToList();
    }

    public List<Doctor> ListDoctorsByYearsExperience()
    {
        return context.Doctors.OrderBy(d => d.YearsExperience).ToList();
    }

    public int GetTotalNumberOfDoctors()
    {
        return context.Doctors.Count();
    }

    public string GetNameOfMostUsedTreatment()
    {
        return context.Treatments
            .OrderByDescending(t => t.PatientTreatments.Count)
            .First().Name;
    }

    public Patient CreatePatient(Patient patient)
    {
        context.Patients.Add(patient);
        context.SaveChanges();
        return patient;
    }
}