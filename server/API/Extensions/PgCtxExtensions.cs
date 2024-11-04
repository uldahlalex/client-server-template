using DataAccess;
using PgCtx;
using Service;

namespace API.Extensions;

public static class PgCtxExtensions
{
    public static WebApplicationBuilder AddPgContainer(this WebApplicationBuilder builder)
    {
        var appOptions = builder.Configuration.GetSection(nameof(AppOptions)).Get<AppOptions>();
        if (appOptions.RunInTestContainer)
        {
            var pg = new PgCtxSetup<HospitalContext>();
            builder.Configuration[nameof(AppOptions) + ":" + nameof(AppOptions.Database)] =
                pg._postgres.GetConnectionString();
        }

        return builder;
    }
}