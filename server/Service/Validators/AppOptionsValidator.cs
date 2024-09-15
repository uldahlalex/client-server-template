using FluentValidation;

namespace Service.Validators;

public class AppOptionsValidator : AbstractValidator<AppOptions>
{
    public AppOptionsValidator()
    {
        RuleFor(x => x.DbConnectionString).NotEmpty();
    }
    
}