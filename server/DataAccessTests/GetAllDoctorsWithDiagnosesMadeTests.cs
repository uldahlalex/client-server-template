using DataAccess;
using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PgCtx;
using SharedTestDependencies;

namespace xunittests.Repository_Tests;

public class GetAllDoctorsWithDiagnosesMadeTests
{
    private readonly HospitalContext _db;
    private readonly PgCtxSetup<HospitalContext> _setup;


    public GetAllDoctorsWithDiagnosesMadeTests()
    {
        _setup = new PgCtxSetup<HospitalContext>();
        _db = _setup.DbContextInstance;
    }
    
    [Fact]
    public void GetAllDoctorsWithDiagnosesMade_ReturnsAllDoctorsWithDiagnoses()
    {
        // Arrange
        var doctor = TestObjects.GetDoctor();
        var patient = TestObjects.GetPatient();
        var disease = TestObjects.GetDisease();
        var diagnosis = TestObjects.GetDiagnosis(doctor, patient, disease);
        _db.AddRange(doctor,patient,disease,diagnosis);
        _db.SaveChanges();
            
        
        
        // A
        var result =_setup.ServiceProviderInstance.GetRequiredService<IHospitalRepository>().GetAllDoctorsIncludingDiagnoses();
        
        // Assert
        var expectedDate = diagnosis.DiagnosisDate;
        var actualDate = result.First().Diagnoses.First().DiagnosisDate;
        Assert.True((actualDate - expectedDate).Value.Duration() < TimeSpan.FromSeconds(1), 
            "The diagnosis dates should be within 1 second of each other.");
    }
}