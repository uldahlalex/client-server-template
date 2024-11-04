using Service;
using Service.Validators;

namespace API.Extensions;

public static class AddAppOptionsExtension
{
    public static WebApplicationBuilder AdddAppOptions(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptionsWithValidateOnStart<AppOptions>()
            .Bind(builder.Configuration.GetSection(nameof(AppOptions)))
            .ValidateDataAnnotations()
            .Validate(options => new AppOptionsValidator().Validate(options).IsValid,
                $"{nameof(AppOptions)} validation failed");
        return builder;
    }
}