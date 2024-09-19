using DataAccess;
using PgCtx;
using Xunit.Abstractions;

namespace ApiInterationTests;

public class DiagnosisTests
{
    private readonly PgCtxSetup<HospitalContext> _pgCtxSetup = new();
    private readonly ITestOutputHelper _outputHelper;

    public DiagnosisTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        Environment.SetEnvironmentVariable("DATABASE", _pgCtxSetup._postgres.GetConnectionString());
    }
    [Fact]
    public async Task CreateDiagnosis_Can_Successfully_Create_Diagnosis()
    {
        
    }
}