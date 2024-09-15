using System.Text.Json;
using DataAccess;
using DataAccess.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        builder.Services.AddDbContext<HospitalContext>((serviceProvider, options) =>
        {
            var appOptions = serviceProvider.GetRequiredService<IOptions<AppOptions>>().Value;
            options.UseNpgsql(appOptions.DbConnectionString);
            options.EnableSensitiveDataLogging();
        });
        builder.Services.AddScoped<IRepository, HospitalRepository>();
        builder.Services.AddScoped<IHospitalService, HospitalService>();
        builder.Services.AddControllers()
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<HospitalService>();
                fv.AutomaticValidationEnabled = false;
            });
        builder.Services.AddOpenApiDocument();

        var app = builder.Build();
        
        var options = app.Services.GetRequiredService<IOptions<AppOptions>>().Value;
        Console.WriteLine("APP OPTIONS: " + JsonSerializer.Serialize(options));
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseStatusCodePages();
        app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        app.UseEndpoints(endpoints => endpoints.MapControllers());
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<HospitalContext>();
            context.Database.EnsureCreated();
        }

        app.UseOpenApi();
        app.UseSwaggerUi();
    }
    
}