using Generated;

namespace ApiInterationTests;

public class DiagnosisTests : ApiTestBase
{
    [Fact]
    public async Task CreateDiagnosis_Can_Successfully_Create_Diagnosis()
    {
        //Arranging
        var disease = TestObjects.GetDisease();
        var doctor = TestObjects.GetDoctor();
        var patient = TestObjects.GetPatient();
        PgCtxSetup.DbContextInstance.Diseases.Add(disease);
        PgCtxSetup.DbContextInstance.Doctors.Add(doctor);
        PgCtxSetup.DbContextInstance.Patients.Add(patient);
        PgCtxSetup.DbContextInstance.SaveChanges();

        //Act
        var request = new CreateDiagnosisDto
        {
            DiseaseId = disease.Id,
            DoctorId = doctor.Id,
            PatientId = patient.Id
        };
        var response = await new DiagnosisClient(TestHttpClient).CreateDiagnosisAsync(request);

        //Assert
        Assert.Equal(200, response.StatusCode);
    }
}