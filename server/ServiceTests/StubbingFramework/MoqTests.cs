using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Service;
using Service.TransferModels.Requests;
using Service.TransferModels.Responses;
using Service.Validators;
using SharedTestDependencies;

namespace ServiceTests.StubbingFramework;

public class MoqTests
{
    private readonly HospitalService _service;
    private readonly Mock<IHospitalRepository> _mockRepo;

    public MoqTests()
    {
        _mockRepo = new Mock<IHospitalRepository>();
        _service = new HospitalService(NullLogger<HospitalService>.Instance, _mockRepo.Object,
            new CreatePatientValidator(), new UpdatePatientValidator(), null);
    }


    [Fact]
    public void CreateNewPatient_Succesfully_CreatesNewPatient()
    {

        var createDto = new CreatePatientDto()
        {
            Address = "asdsad",
            Birthdate = DateOnly.MaxValue,
            Gender = true,
            Name = "Bob33"
        };
        var patient = TestObjects.GetPatient();


        _mockRepo.Setup(x => x.InsertPatient(It.IsAny<Patient>())).Returns(patient);
        var act = _service.CreatePatient(createDto);
        Assert.Equal(act.Name, new PatientDto().FromEntity(patient).Name);
    }
}