using Microsoft.Extensions.Logging.Abstractions;
using Service;
using Service.TransferModels.Requests;
using Service.Validators;

namespace ServiceTests;

public class StubbedTests
{

    private readonly IHospitalService _service = new HospitalService(NullLogger<HospitalService>.Instance, new StubHospitalRepository(), new CreatePatientValidator(), new UpdatePatientValidator(), null);
    
    [Fact]
    public void CreatePatient_With_Invalid_Data_Throws_Exception()
    {
        var patient = new CreatePatientDto();
        Assert.ThrowsAny<Exception>(() => _service.CreatePatient(patient));
    }
    
}