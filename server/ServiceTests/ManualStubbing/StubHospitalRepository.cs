using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace ServiceTests;

public class StubHospitalRepository : IHospitalRepository
{
    public List<Doctor> GetAllDoctors()
    {
        throw new NotImplementedException();
    }

    public List<Doctor> ListDoctorsByYearsExperience()
    {
        throw new NotImplementedException();
    }

    public int GetTotalNumberOfDoctors()
    {
        throw new NotImplementedException();
    }

    public List<Doctor> GetAllDoctorsIncludingDiagnoses()
    {
        throw new NotImplementedException();
    }

    public Doctor GetDoctorWhoMadeMostDiagnoses()
    {
        throw new NotImplementedException();
    }

    public List<Doctor> GetAllDoctorsWithSpecialty(string specialty)
    {
        throw new NotImplementedException();
    }

    public List<Patient> GetAllPatientsWhoHasHadTreatmentWithId(int treatmentId)
    {
        throw new NotImplementedException();
    }

    public string GetNameOfMostUsedTreatment()
    {
        throw new NotImplementedException();
    }

    public Patient InsertPatient(Patient patient)
    {
        patient.Id = 1;
        patient.Diagnoses = new List<Diagnosis>();
        patient.PatientTreatments = new List<PatientTreatment>();
        return patient;
    }

    public Doctor GetDoctorById(int doctorId)
    {
        throw new NotImplementedException();
    }
}