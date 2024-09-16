using DataAccess;
using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PgCtx;
using SharedTestDependencies;

namespace xunittests.Repository_Tests;

public class GetDoctorWhoMadeMostDiagnosesTests
{
    private readonly PgCtxSetup<HospitalContext>        _setup = new PgCtxSetup<HospitalContext>();


    [Fact]
    public void GetDoctorWhoMadeMostDiagnoses_ShouldFindDoctorWithMostDiagnoses()
    {
        //Arrange
        var doctor1 = Constants.GetDoctor();
        var doctor2 = Constants.GetDoctor();
        var disease = Constants.GetDisease();
        var diagnosis1 = Constants.GetDiagnosis(doctor1, Constants.GetPatient(), disease);
        var diagnosis2 = Constants.GetDiagnosis(doctor1, Constants.GetPatient(), disease);
        var diagnosis3 = Constants.GetDiagnosis(doctor2, Constants.GetPatient(), disease);
        _setup.DbContextInstance.AddRange(doctor1, doctor2, disease, diagnosis1, diagnosis2, diagnosis3);
        _setup.DbContextInstance.SaveChanges();
        
        //Act
        var result = _setup.ServiceProviderInstance.GetRequiredService<IHospitalRepository>().GetDoctorWhoMadeMostDiagnoses();
        
        //Assert
        Assert.Equal(doctor1.Id, result.Id);
    }
}