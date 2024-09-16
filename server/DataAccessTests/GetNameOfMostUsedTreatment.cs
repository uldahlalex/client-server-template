using DataAccess;
using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PgCtx;
using SharedTestDependencies;

namespace xunittests.Repository_Tests;

public class GetNameOfMostUsedTreatment
{
    private PgCtxSetup<HospitalContext>         _setup = new PgCtxSetup<HospitalContext>();


    [Fact]
    public void GetNameOfMostUsedTreatment_Correctly_FindsName()
    {
        //Arrange
        var treatment = Constants.GetTreatment();
        var treatment2 = Constants.GetTreatment();
        var patient = Constants.GetPatient();
        var patient2 = Constants.GetPatient();
        var pt = Constants.GetPatientTreatment(patient, treatment);
        var pt2 = Constants.GetPatientTreatment(patient2, treatment);
        var pt3 = Constants.GetPatientTreatment(patient2, treatment2);
        
        
        _setup.DbContextInstance.AddRange(treatment, treatment2, patient, patient2, pt, pt2, pt3);
        _setup.DbContextInstance.SaveChanges();
        
        //Act
        var result =_setup.ServiceProviderInstance.GetRequiredService<IHospitalRepository>().GetNameOfMostUsedTreatment();
        
        //Assert
        Assert.Equal(result, treatment.Name);
    }
}