using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NSubstitute;
using Service;
using Service.TransferModels.Requests;
using Service.Validators;

namespace ServiceTests.StubbingFramework;

public class NSubstitutedHospitalServiceTests
{
    private readonly IHospitalService _hospitalService;
    private readonly IHospitalRepository _mockRepo;

    public NSubstitutedHospitalServiceTests()
    {
        _mockRepo = Substitute.For<IHospitalRepository>();

    }


    /// <summary>
    /// this method tests that a valid object does not trigger data validation exception
    /// and that the method returns desired object type
    /// </summary>
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
        
        _mockRepo.InsertPatient(Arg.Any<Patient>()).Returns(expectedPatient); //Configuring the mock call

        var result = _hospitalService.CreatePatient(createPatientDto); //In here the mock call is used

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