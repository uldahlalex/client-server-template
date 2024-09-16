using DataAccess;
using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PgCtx;
using SharedTestDependencies;

namespace xunittests.Repository_Tests;

public class GetAllDoctorsWithSpecialityTests
{
    public PgCtxSetup<HospitalContext> _setup  = new PgCtxSetup<HospitalContext>();


    [Fact]
    public void GetAllDoctorsWithSpeciality_Returns_Doctors_WithSpeciality()
    {
        //Arrange
        var doctor1 = Constants.GetDoctor();
        var doctor2 = Constants.GetDoctor();
        
        doctor1.Specialty = "Cardiologist";
        doctor2.Specialty = "Urologist";
        
        _setup.DbContextInstance.AddRange(doctor1, doctor2);
        _setup.DbContextInstance.SaveChanges();
        
        //Act
        var result = _setup.ServiceProviderInstance.GetRequiredService<IHospitalRepository>().GetAllDoctorsWithSpecialty("Cardiologist");
        
        //Assert
        Assert.Equivalent(result.First(), doctor1);
    }
}