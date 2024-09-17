using DataAccess.Interfaces;
using DataAccess.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using NSubstitute.Exceptions;
using Service;
using Service.TransferModels.Requests;
using Service.Validators;

namespace ServiceTests.StubbingFramework;

public class HospitalServiceTests
{
    private readonly HospitalService _hospitalService;
    private readonly IHospitalRepository _mockRepo;

    public HospitalServiceTests()
    {
        _mockRepo = Substitute.For<IHospitalRepository>();
        _hospitalService = new HospitalService(NullLogger<HospitalService>.Instance, _mockRepo,
            new CreatePatientValidator(), new UpdatePatientValidator(), null);
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

        var expectedPatient = new Patient
        {
            Id = 1,
            Name = createPatientDto.Name,
            Address = createPatientDto.Address,
            Birthdate = createPatientDto.Birthdate,
            Gender = createPatientDto.Gender
        };

        _mockRepo.CreatePatient(Arg.Any<Patient>()).Returns(expectedPatient);

        var result = _hospitalService.CreatePatient(createPatientDto);

        Assert.NotNull(result);
        Assert.Equal(expectedPatient.Id, result.Id);
        Assert.Equal(expectedPatient.Name, result.Name);
        Assert.Equal(expectedPatient.Address, result.Address);
        Assert.Equal(expectedPatient.Birthdate, result.Birthdate);
        Assert.Equal(expectedPatient.Gender, result.Gender);
        
    }
    
    
    [Fact]
    public void CreatePatient_Should_TriggerDataValidation_For_Too_Short_Name()
    {
        var createPatientDto = new CreatePatientDto
        {
            Name = "Jo",
            Address = "1234 Elm Street",
            Birthdate = new DateOnly(1990, 1, 1),
            Gender = true
        };
        Assert.ThrowsAny<ValidationException>(() => _hospitalService.CreatePatient(createPatientDto));
    }

}