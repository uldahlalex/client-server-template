using DataAccess;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using PgCtx;
using Service;
using Service.TransferModels.Requests;
using Service.Validators;

namespace ServiceTests;

public class ManualStubHospitalServiceTests
{
    private readonly HospitalService _hospitalService;

    public ManualStubHospitalServiceTests()
    {
        _hospitalService = new HospitalService(NullLogger<HospitalService>.Instance, 
            new StubHospitalRepository(),
            new CreatePatientValidator(), 
            new UpdatePatientValidator(), 
            null);
    }

    [Fact]
    public void CreatePatient_Should_Successfully_Return_A_Patient()
    {
        var createPatientDto = new CreatePatientDto
        {
            Name = "John Doe",
            Address = "1234 Elm Street",
            Birthdate = new DateOnly(1990, 1, 1),
            Gender = true
        };
        var result = _hospitalService.CreatePatient(createPatientDto);
        Assert.Equal(1, result.Id);
        Assert.Equal("John Doe", result.Name);
        Assert.Equal("1234 Elm Street", result.Address);
        Assert.Equal(new DateOnly(1990, 1, 1), result.Birthdate);
    }
    
}