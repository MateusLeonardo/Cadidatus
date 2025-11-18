using Candidatus.Communication.Requests;
using Candidatus.Exceptions;
using FluentValidation;

namespace Candidatus.Application.UseCases.Company.Update;

public class UpdateCompanyValidator : AbstractValidator<RequestUpdateCompanyJson>
{
    public UpdateCompanyValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage(ResourceMessagesExceptions.COMPANY_EMPTY)
            .MaximumLength(255).WithMessage(ResourceMessagesExceptions.COMPANY_LENGTH_INVALID);
            
        RuleFor(r => r.CityId).GreaterThan(0).WithMessage(ResourceMessagesExceptions.CITY_INVALID);
    }
}