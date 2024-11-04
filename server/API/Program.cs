using System.Text.Json;
using DataAccess;
using DataAccess.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PgCtx;
using Service;
using Service.Validators;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOptionsWithValidateOnStart<AppOptions>()
            .Bind(builder.Configuration.GetSection(nameof(AppOptions)))
            .ValidateDataAnnotations()
            .Validate(options => new AppOptionsValidator().Validate(options).IsValid,
                $"{nameof(AppOptions)} validation failed");

        var appOptions = builder.Configuration.GetSection(nameof(AppOptions)).Get<AppOptions>();
        if (appOptions.RunInTestContainer)
        {
            var pg = new PgCtxSetup<HospitalContext>();
            builder.Configuration[nameof(AppOptions) + ":" + nameof(AppOptions.Database)] =
                pg._postgres.GetConnectionString();
        }


        builder.Services.AddDbContext<HospitalContext>((serviceProvider, options) =>
        {
            var appOptions = serviceProvider.GetRequiredService<IOptions<AppOptions>>().Value;
            options.UseNpgsql(appOptions.Database);
            options.EnableSensitiveDataLogging();
        });
        builder.Services.AddFluentValidation(
            fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePatientValidator>());
        builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
        builder.Services.AddScoped<IHospitalService, HospitalService>();
        builder.Services.AddControllers();
        builder.Services.AddOpenApiDocument();

        var app = builder.Build();

        var options = app.Services.GetRequiredService<IOptions<AppOptions>>().Value;
        Console.WriteLine("APP OPTIONS: " + JsonSerializer.Serialize(options));

        app.UseHttpsRedirection();

        app.UseRouting();


        app.UseOpenApi();
        app.UseSwaggerUi();
        app.UseStatusCodePages();

        app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        app.UseEndpoints(endpoints => endpoints.MapControllers());

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<HospitalContext>();
            context.Database.EnsureCreated();
            //Writes current SQL database to a .sql file
            File.WriteAllText("current_db.sql", context.Database.GenerateCreateScript());
        }

        app.Run();
    }
}