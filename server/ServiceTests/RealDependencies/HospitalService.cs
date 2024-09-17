using DataAccess;
using DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Npgsql.Internal;
using PgCtx;
using Service;
using Service.Validators;
using SharedTestDependencies;

namespace ServiceTests.RealDependencies;

public class HospitalServiceTests
{
     private readonly PgCtxSetup<HospitalContext> _pgCtxSetup = new();
     private readonly HospitalService _hospitalService;

     public HospitalServiceTests()
     {
          IHospitalRepository repository = new HospitalRepository(_pgCtxSetup.DbContextInstance);
          ILogger<HospitalService> logger = LoggerFactory.Create((builder) => builder.AddConsole()).CreateLogger<HospitalService>();
          CreatePatientValidator createPatientValidator = new CreatePatientValidator();
          UpdatePatientValidator updatePatientValidator = new UpdatePatientValidator();
          _hospitalService = new HospitalService(logger,  repository,createPatientValidator, updatePatientValidator,
               _pgCtxSetup.DbContextInstance);

     }

     [Fact]
     public void GetAllPatients_Can_Limit()
     {
         _pgCtxSetup.DbContextInstance.Patients.Add(TestObjects.GetPatient());
         _pgCtxSetup.DbContextInstance.Patients.Add(TestObjects.GetPatient());
         _pgCtxSetup.DbContextInstance.Patients.Add(TestObjects.GetPatient());
         _pgCtxSetup.DbContextInstance.SaveChanges();
         
         var act = _hospitalService.GetAllPatients(2, 0);
         var actualLength = act.Count;
         var expectedLength = 2;
         Assert.Equivalent(expectedLength, actualLength);
     }
     
}