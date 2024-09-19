using DataAccess;
using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PgCtx;
using SharedTestDependencies;

namespace xunittests.Repository_Tests;

public class GetNameOfMostUsedTreatment
{
    private readonly PgCtxSetup<HospitalContext>         _setup = new PgCtxSetup<HospitalContext>(configureServices: services => services.AddScoped<IHospitalRepository, HospitalRepository>());


    [Fact]
    public void GetNameOfMostUsedTreatment_Correctly_FindsName()
    {
        //Arrange
        var treatment = TestObjects.GetTreatment();
        var treatment2 = TestObjects.GetTreatment();
        var patient = TestObjects.GetPatient();
        var patient2 = TestObjects.GetPatient();
        var pt = TestObjects.GetPatientTreatment(patient, treatment);
        var pt2 = TestObjects.GetPatientTreatment(patient2, treatment);
        var pt3 = TestObjects.GetPatientTreatment(patient2, treatment2);
        
        
        _setup.DbContextInstance.AddRange(treatment, treatment2, patient, patient2, pt, pt2, pt3);
        _setup.DbContextInstance.SaveChanges();
        
        //Act
        var result =_setup.ServiceProviderInstance.GetRequiredService<IHospitalRepository>().GetNameOfMostUsedTreatment();
        
        //Assert
        Assert.Equal(result, treatment.Name);
    }
}