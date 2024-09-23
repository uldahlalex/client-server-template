using System.Text.Json;
using DataAccess;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<DunderContext>((serviceProvider, options) =>
        {
            var appOptions = serviceProvider.GetRequiredService<IOptions<AppOptions>>().Value;
            options.UseNpgsql(Environment.GetEnvironmentVariable("DbConnectionString") 
                              ?? appOptions.DbConnectionString);
            options.EnableSensitiveDataLogging();
        });
        //builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
        //builder.Services.AddScoped<IHospitalService, Service.Service>();
        builder.Services.AddControllers();
        builder.Services.AddOpenApiDocument();

        var app = builder.Build();
        app.UseHttpsRedirection();

        app.UseRouting();


        app.UseOpenApi();
        app.UseSwaggerUi();
        app.UseStatusCodePages();

        app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        app.UseEndpoints(endpoints => endpoints.MapControllers());

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DunderContext>();
            context.Database.EnsureCreated();
        }

        app.Run();
    }
    
}