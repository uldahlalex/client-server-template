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
        TestHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserJwt);
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
        "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAiLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMDAiLCJleHAiOjE3MzI2OTUyMzUsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJhbGV4QHVsZGFobC5kayIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiMmJiMjFlOWEtZmMwYi00Y2RlLTgyZjctZGM2YWJmYTYxNzAyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiUmVhZGVyIiwiaWF0IjoxNzMyMDkwNDM1LCJuYmYiOjE3MzIwOTA0MzV9.G5wYJZHXwF4-vSOoKG0Pa3MB6fxnaaL5RjtQ-cYhH2K4Mzn_SVrB0bN8Aa1LHbiV1DYxyezbOozeC82C7n5a5w";

    public IServiceProvider ApplicationServices { get; set; }

    #endregion
}