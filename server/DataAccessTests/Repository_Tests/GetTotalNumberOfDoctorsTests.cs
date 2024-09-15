using DataAccess;
using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PgCtx;
using SharedTestDependencies;

namespace xunittests.Repository_Tests;

public class GetTotalNumberOfDoctorsTests
{
    private PgCtxSetup<HospitalContext> _setup = TestSetupHelper.CreateTestSetup();

    [Fact]
    public void GetTotalNumberOfDoctors_Correctly_Finds_Total_Number_Of_Doctors()
    {
        //Arrange

        var doctor = Constants.GetDoctor();
        var doctor2 = Constants.GetDoctor();
        
        _setup.DbContextInstance.AddRange(doctor, doctor2);
        _setup.DbContextInstance.SaveChanges();
        
        //Act
        var result = _setup.ServiceProviderInstance.GetRequiredService<IRepository>().GetTotalNumberOfDoctors();
        
        //Assert
        Assert.Equal(2, result);
    }
}