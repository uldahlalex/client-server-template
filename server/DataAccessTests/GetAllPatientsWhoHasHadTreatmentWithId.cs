using DataAccess;
using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PgCtx;
using SharedTestDependencies;

namespace xunittests.Repository_Tests;

public class GetAllPatientsWhoHasHadTreatmentWithId
{
    
    private readonly PgCtxSetup<HospitalContext>         _setup = new PgCtxSetup<HospitalContext>();



    [Fact]
    public void GetAllPatientsWhoHasTreatmentWithId_Correctly_Returns_List()
    {
        //Arrange
        var patient = TestObjects.GetPatient();
        var patient2 = TestObjects.GetPatient();
        var treatment = TestObjects.GetTreatment();
        var patientTreatment = TestObjects.GetPatientTreatment(patient, treatment);
        _setup.DbContextInstance.AddRange(patient, patient2, treatment, patientTreatment);
        _setup.DbContextInstance.SaveChanges();
        
        //Act
        var result = _setup.ServiceProviderInstance.GetRequiredService<IHospitalRepository>().GetAllPatientsWhoHasHadTreatmentWithId(treatment.Id);
        
        //Assert
        Assert.Equivalent(result.First(), patient);

    }
}