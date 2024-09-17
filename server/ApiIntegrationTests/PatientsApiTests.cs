using System.Text.Json;
using API;
using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using PgCtx;
using SharedTestDependencies;
using Xunit.Abstractions;

namespace ApiInterationTests;

public class PatientsApiTests(ITestOutputHelper outputHelper) : WebApplicationFactory<Program>
{
    private readonly PgCtxSetup<HospitalContext> _pgCtxSetup = new();
    
    [Theory]
    [InlineData(5,3)]
    public async Task GetAllPatients_Pagination_Can_Limit_And_Skip(int startAt, int limit)
    {
        var patients = new List<Patient>();
        for (var i = 0; i < 10; i++)
        {
            var p = TestObjects.GetPatient();
            patients.Add(p);
        
            var entry = _pgCtxSetup.DbContextInstance.Patients.Attach(p);
            entry.State = EntityState.Added;
        }
        await _pgCtxSetup.DbContextInstance.SaveChangesAsync();
        
        var patientsResponse = await CreateClient()
            .GetAsync($"api/{nameof(Patient)}?startAt={startAt}&limit={limit}")
            .Result.Content
            .ReadAsStringAsync();
    
        var patientsList = JsonSerializer.Deserialize<List<Patient>>(patientsResponse, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true  
        });
    
        var expected = patients.OrderBy(p => p.Id).Skip(startAt).Take(limit).ToList();
        
        Assert.Equivalent(expected.Select(p => p.Id), patientsList.Select(p => p.Id));
    }

    [Fact]
    public async Task GetAllPatients_Can_Get_All_Patients_And_Status_OK()
    {
        var patient = TestObjects.GetPatient();
        _pgCtxSetup.DbContextInstance.Patients.Add(patient);
        _pgCtxSetup.DbContextInstance.SaveChanges();

        var response = await CreateClient().GetAsync("/api/Patient").Result.Content.ReadAsStringAsync();
        var returnedPatient = JsonSerializer.Deserialize<List<Patient>>(response, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

        outputHelper.WriteLine(response);
        outputHelper.WriteLine(JsonSerializer.Serialize(patient));
        var patientList = new List<Patient>() { patient };
        Assert.Equal(patientList, returnedPatient);
        
    }

}