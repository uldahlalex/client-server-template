using DataAccess;
using DataAccess.Models;
using Microsoft.Extensions.DependencyInjection;
using PgCtx;
using SharedTestDependencies;
using Xunit.Abstractions;

namespace xunittests.Repository_Tests;

public class GetAllDoctorsTest
{
    private readonly PgCtxSetup<HospitalContext> _setup = new();
    
    [Fact]
    public void GetAllDoctors_ReturnsAllDoctors()
    {
        var doctors = new List<Doctor>
        {
            Constants.GetDoctor(), //This is a method that returns a doctor object
            Constants.GetDoctor() //This is a method that returns a doctor object
        };
        _setup.DbContextInstance.Doctors.AddRange(doctors);
        _setup.DbContextInstance.SaveChanges();

        var result = new HospitalRepository(_setup.DbContextInstance).GetAllDoctors();

        Assert.Equivalent(doctors, result);
    }
}


public class GetAllDoctorsTestWithServiceCollection(ITestOutputHelper outputHelper)
{
    private readonly PgCtxSetup<HospitalContext> _setup = new(configureServices: 
        services =>
        {
            services.AddTransient<HospitalRepository>();
        });
    [Fact]
    public void GetAllDoctors_ReturnsAllDoctors()
    {
        outputHelper.WriteLine("hello world");
        var doctors = new List<Doctor>
        {
            Constants.GetDoctor(), //This is a method that returns a doctor object
            Constants.GetDoctor() //This is a method that returns a doctor object
        };
        _setup.DbContextInstance.Doctors.AddRange(doctors);
        _setup.DbContextInstance.SaveChanges();

        var result = _setup.ServiceProviderInstance.GetRequiredService<HospitalRepository>().GetAllDoctors();

        Assert.Equivalent(doctors, result);
    }
}

