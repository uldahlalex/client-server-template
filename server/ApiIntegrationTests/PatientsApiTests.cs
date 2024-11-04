// using System.Net;
// using System.Net.Http.Json;
// using System.Text.Json;
// using API;
// using DataAccess.Models;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Microsoft.EntityFrameworkCore;
//
// namespace ApiInterationTests;
//
// public class PatientsApiTests : WebApplicationFactory<Program>
// {
//     [Theory]
//     [InlineData(5, 3)]
//     public async Task GetAllPatients_Pagination_Can_Limit_And_Skip(int startAt, int limit)
//     {
//         var patients = new List<Patient>();
//         for (var i = 0; i < 10; i++)
//         {
//             var p = TestObjects.GetPatient();
//             patients.Add(p);
//
//             var entry = _pgCtxSetup.DbContextInstance.Patients.Attach(p);
//             entry.State = EntityState.Added;
//         }
//
//         await _pgCtxSetup.DbContextInstance.SaveChangesAsync();
//
//         var patientsResponse = await CreateClient()
//             .GetAsync($"api/{nameof(Patient)}" +
//                       $"?startAt={startAt}&limit={limit}")
//             .Result.Content
//             .ReadAsStringAsync();
//
//         var patientsList = JsonSerializer.Deserialize<List<Patient>>(patientsResponse,
//             new JsonSerializerOptions
//             {
//                 PropertyNameCaseInsensitive = true
//             });
//         var expected = patients.OrderBy(p => p.Id).Skip(startAt).Take(limit).ToList();
//         Assert.Equivalent(expected.Select(p => p.Id), patientsList.Select(p => p.Id));
//     }
//
//     [Fact]
//     public async Task GetAllPatients_Can_Get_All_Patients_And_Status_OK()
//     {
//         var patient = TestObjects.GetPatient();
//         _pgCtxSetup.DbContextInstance.Patients.Add(patient);
//         _pgCtxSetup.DbContextInstance.SaveChanges();
//
//         var response = await CreateClient().GetAsync("/api/Patient");
//
//         var returnedPatient = JsonSerializer.Deserialize<List<Patient>>(
//             await response.Content.ReadAsStringAsync(),
//             new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
//
//         var patientList = new List<Patient> { patient };
//         Assert.Equivalent(patientList, returnedPatient);
//         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//     }
//
//     [Fact]
//     public async Task CreatePatient_Can_Persist_Patient_To_DB()
//     {
//         var testPatient = TestObjects.GetPatient();
//
//         var act = await CreateClient()
//             .PostAsJsonAsync("/api/" + nameof(Patient),
//                 testPatient);
//
//         var returnedPatient = JsonSerializer.Deserialize<Patient>(
//             await act.Content.ReadAsStringAsync(),
//             new JsonSerializerOptions
//             {
//                 PropertyNameCaseInsensitive = true
//             }) ?? throw new InvalidOperationException();
//
//         var patientInDb = _pgCtxSetup.DbContextInstance.Patients.First();
//         Assert.Equal(testPatient.Name, returnedPatient.Name);
//         Assert.Equal(testPatient.Birthdate, returnedPatient.Birthdate);
//         Assert.Equal(testPatient.Address, returnedPatient.Address);
//         Assert.Equal(testPatient.Gender, returnedPatient.Gender);
//         Assert.Equivalent(testPatient.Birthdate, patientInDb.Birthdate);
//         Assert.Equivalent(testPatient.Gender, patientInDb.Gender);
//         Assert.Equivalent(testPatient.Address, patientInDb.Address);
//         Assert.Equivalent(testPatient.Name, patientInDb.Name);
//     }
// }

