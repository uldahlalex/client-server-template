using System.Text.Json;
using DataAccess;
using DataAccess.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service;
using Service.Interfaces;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.Configure<AppOptions>(builder.Configuration.GetSection(nameof(AppOptions)));
        builder.Services.AddOptions<AppOptions>()
            .Bind(builder.Configuration.GetSection(nameof(AppOptions)))
            .ValidateDataAnnotations()
            .Validate(appOptions => !string.IsNullOrEmpty(appOptions.DbConnectionString), 
                "DbConnectionString must be provided.");
        
        builder.Services.AddDbContext<DunderContext>((serviceProvider, options) =>
        {
            var appOptions = serviceProvider.GetRequiredService<IOptions<AppOptions>>().Value;
            options.UseNpgsql(Environment.GetEnvironmentVariable("DbConnectionString") 
                              ?? appOptions.DbConnectionString);
            options.EnableSensitiveDataLogging();
        });
        builder.Services.AddScoped<IPaperRepository, PaperRepository>();
        builder.Services.AddScoped<IPaperService, PaperService>();
        builder.Services.AddControllers();
        builder.Services.AddOpenApiDocument();

        var app = builder.Build();
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseOpenApi();
        app.UseSwaggerUi();
        app.UseStatusCodePages();

        app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DunderContext>();
            context.Database.EnsureCreated();
        }

        app.Run();
    }
    
}