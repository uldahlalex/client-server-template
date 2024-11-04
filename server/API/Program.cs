using System.Text.Json;
using API.Extensions;
using DataAccess;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NSwag;
using NSwag.Generation.Processors.Security;
using PgCtx;
using Service;
using Service.Validators;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AdddAppOptions();

        builder.AddPgContainer();


        builder.Services.AddDbContext<HospitalContext>((serviceProvider, options) =>
        {
            var appOptions = serviceProvider.GetRequiredService<IOptions<AppOptions>>().Value;
            options.UseNpgsql(appOptions.Database);
            options.EnableSensitiveDataLogging();   
        });
        builder.Services.AddFluentValidation(
            fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePatientValidator>());
        builder.Services.AddScoped<IHospitalService, HospitalService>();
        builder.Services.AddControllers();
        builder.Services.AddOpenApiDocument(configuration =>
        {

            {
                configuration.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });
                //configuration.AddTypeToSwagger<T>(); //If you need to add some type T to the Swagger known types
                configuration.DocumentProcessors.Add(new MakeAllPropertiesRequiredProcessor());

                configuration.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            }
        });

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