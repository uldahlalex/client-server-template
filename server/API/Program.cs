using API.Extensions;
using DataAccess;
using DataAccess.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSwag;
using NSwag.Generation.Processors.Security;
using Service;
using Service.Security;
using Service.Validators;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AdddAppOptions();

        builder.AddPgContainer();
        var options = builder.Configuration.GetSection(nameof(AppOptions)).Get<AppOptions>()!;


        builder.Services.AddDbContext<HospitalContext>(config =>
        {
            config.UseNpgsql(options.DbConnectionString);
            config.EnableSensitiveDataLogging();
        });
        builder.Services.AddFluentValidation(
            fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePatientValidator>());
        builder.Services.AddScoped<IHospitalService, HospitalService>();

        #region Security

        builder
            .Services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<HospitalContext>();
        builder.Services.AddSingleton<IPasswordHasher<User>, Argon2idPasswordHasher<User>>();
        builder
            .Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o => { o.TokenValidationParameters = JwtTokenClaimService.ValidationParameters(options); });
        builder.Services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                // Globally require users to be authenticated
                .RequireAuthenticatedUser()
                .Build();
        });
        builder.Services.AddScoped<ITokenClaimsService, JwtTokenClaimService>();

        #endregion

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

        //app.UseHttpsRedirection();

        app.UseRouting();
        //app.UseAuthentication();
        //app.UseAuthorization();

        app.MapIdentityApi<IdentityUser>().AllowAnonymous();


        app.UseOpenApi();
        app.UseSwaggerUi();
        app.UseStatusCodePages();

        app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        app.UseEndpoints(endpoints => endpoints.MapControllers());

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<HospitalContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            //Writes current SQL database to a .sql file
            File.WriteAllText("current_db.sql", context.Database.GenerateCreateScript());
        }

        app.Run();
    }
}