using Candidatus.Communication.Requests;
using Candidatus.Exceptions;
using FluentValidation;

namespace Candidatus.Application.UseCases.City.Update;

public class UpdateCityValidator : AbstractValidator<RequestUpdateCityJson> {
    public UpdateCityValidator() {
        RuleFor(r => r.Name).NotEmpty().WithMessage(ResourceMessagesExceptions.CITY_EMPTY);
        RuleFor(r => r.StateId).GreaterThan(0).WithMessage(ResourceMessagesExceptions.STATE_INVALID);
    }
}