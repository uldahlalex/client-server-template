using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface IHospitalRepository
{
    public List<Doctor> GetAllDoctors();

    /// <summary>
    ///     Order ascending
    /// </summary>
    /// <returns></returns>
    public List<Doctor> ListDoctorsByYearsExperience();

    public int GetTotalNumberOfDoctors();

    /// <summary>
    ///     Diagnoses are a property for a doctor object which must be included here
    /// </summary>
    /// <returns></returns>
    public List<Doctor> GetAllDoctorsIncludingDiagnoses();

    public Doctor GetDoctorWhoMadeMostDiagnoses();
    public List<Doctor> GetAllDoctorsWithSpecialty(string specialty);


    public List<Patient> GetAllPatientsWhoHasHadTreatmentWithId(int treatmentId);

    public string GetNameOfMostUsedTreatment();
    public Patient InsertPatient(Patient patient);
    public Doctor GetDoctorById(int doctorId);
}