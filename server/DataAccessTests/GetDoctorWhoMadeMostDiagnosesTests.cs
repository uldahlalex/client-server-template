using DataAccess;
using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PgCtx;
using SharedTestDependencies;

namespace xunittests.Repository_Tests;

public class GetDoctorWhoMadeMostDiagnosesTests
{
    private readonly PgCtxSetup<HospitalContext>         _setup = new PgCtxSetup<HospitalContext>(configureServices: services => services.AddScoped<IHospitalRepository, HospitalRepository>());


    [Fact]
    public void GetDoctorWhoMadeMostDiagnoses_ShouldFindDoctorWithMostDiagnoses()
    {
        //Arrange
        var doctor1 = TestObjects.GetDoctor();
        var doctor2 = TestObjects.GetDoctor();
        var disease = TestObjects.GetDisease();
        var diagnosis1 = TestObjects.GetDiagnosis(doctor1, TestObjects.GetPatient(), disease);
        var diagnosis2 = TestObjects.GetDiagnosis(doctor1, TestObjects.GetPatient(), disease);
        var diagnosis3 = TestObjects.GetDiagnosis(doctor2, TestObjects.GetPatient(), disease);
        _setup.DbContextInstance.AddRange(doctor1, doctor2, disease, diagnosis1, diagnosis2, diagnosis3);
        _setup.DbContextInstance.SaveChanges();
        
        //Act
        var result = _setup.ServiceProviderInstance.GetRequiredService<IHospitalRepository>().GetDoctorWhoMadeMostDiagnoses();
        
        //Assert
        Assert.Equal(doctor1.Id, result.Id);
    }
}