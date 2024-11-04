using FluentValidation;
using Service.TransferModels.Requests;

namespace Service.Validators;

public class CreatePatientValidator : AbstractValidator<CreatePatientDto>
{
    public CreatePatientValidator()
    {
        RuleFor(p => p.Name.Length).GreaterThan(3);
    }
}