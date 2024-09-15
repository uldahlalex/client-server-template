using FluentValidation;
using Service.TransferModels.Requests;

namespace Service.Validators;

public class UpdatePatientValidator : AbstractValidator<UpdatePatientDto>
{
    public UpdatePatientValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Birthdate).NotEmpty();
    }
}