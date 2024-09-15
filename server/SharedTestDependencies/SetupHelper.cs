using DataAccess;
using DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using PgCtx;

namespace SharedTestDependencies;

public static class TestSetupHelper
{
    public static PgCtxSetup<HospitalContext> CreateTestSetup()
    {
        bool useSolution = Environment.GetEnvironmentVariable("USE_SOLUTION")?.ToLower() == "true";

        return new PgCtxSetup<HospitalContext>(configureServices: services =>
        {
            services.AddTransient<IRepository, HospitalRepository>();
                
        });
    }
}