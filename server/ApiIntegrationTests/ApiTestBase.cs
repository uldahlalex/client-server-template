using System.Net.Http.Headers;
using API;
using DataAccess;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PgCtx;
using Service;

namespace ApiInterationTests;

public class ApiTestBase : WebApplicationFactory<Program>
{
    public ApiTestBase()
    {
        PgCtxSetup = new PgCtxSetup<HospitalContext>();
        Environment.SetEnvironmentVariable(nameof(AppOptions) + ":" + nameof(AppOptions.DbConnectionString),
            PgCtxSetup._postgres.GetConnectionString());
        ApplicationServices = base.Services.CreateScope().ServiceProvider;
        TestHttpClient = CreateClient();
        //If you have enabled authentication, you can attach a default JWT for the http client
        TestHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(UserJwt);
        Seed().GetAwaiter().GetResult();
    }

    /// <summary>
    ///     Data that will be populated before each test
    /// </summary>
    public async Task Seed()
    {
        var ctx = ApplicationServices.GetRequiredService<HospitalContext>();

        //here you can seed some "default" test objects to the database before each test runs

        ctx.SaveChanges();
    }


    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<HospitalContext>));

            if (descriptor != null) services.Remove(descriptor);

            services.AddDbContext<HospitalContext>(opt =>
            {
                opt.UseNpgsql(PgCtxSetup._postgres.GetConnectionString());
                opt.EnableSensitiveDataLogging(false);
                opt.LogTo(_ => { });
            });
        });
        return base.CreateHost(builder);
    }

    #region properties

    public PgCtxSetup<HospitalContext> PgCtxSetup;
    public HttpClient TestHttpClient { get; set; }

    public string UserJwt { get; set; } =
        "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTUxNjIzOTAyMn0.0Bk7pFvb2zgnomw3gUNpoCNq9fEhAD-qrzD38eOjo4PN0PZwiZbcssGRuslR0KG9umsY1lB0MFCH54eRSficnQ";

    public IServiceProvider ApplicationServices { get; set; }

    #endregion
}