using Candidatus.Communication.Requests;
using Candidatus.Exceptions;
using FluentValidation;

namespace Candidatus.Application.UseCases.City.Register;

public class RegisterCityValidator : AbstractValidator<RequestRegisterCityJson>
{
    public RegisterCityValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage(ResourceMessagesExceptions.CITY_EMPTY);
        RuleFor(r => r.StateId).GreaterThan(0).WithMessage(ResourceMessagesExceptions.STATE_INVALID);
    }
}
