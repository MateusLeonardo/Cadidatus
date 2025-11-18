using Candidatus.Communication.Requests;
using Candidatus.Exceptions;
using FluentValidation;

namespace Candidatus.Application.UseCases.Company.Register;
public class RegisterCompanyValidator : AbstractValidator<RequestRegisterCompanyJson>
{
    public RegisterCompanyValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage(ResourceMessagesExceptions.COMPANY_EMPTY)
            .MaximumLength(255).WithMessage(ResourceMessagesExceptions.COMPANY_LENGTH_INVALID);
        RuleFor(r => r.CityId).GreaterThan(0).WithMessage(ResourceMessagesExceptions.CITY_INVALID);
    }
}
