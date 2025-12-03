using Candidatus.Communication.Requests;
using Candidatus.Domain.Extensions;
using Candidatus.Exceptions;
using FluentValidation;

namespace Candidatus.Application.UseCases.Application.Register;

public class RegisterApplicationValidator: AbstractValidator<RequestRegisterApplicationJson>
{
    public RegisterApplicationValidator()
    {
        RuleFor(r => r.Title).NotEmpty().WithMessage(ResourceMessagesExceptions.APPLICATION_TITLE_EMPTY);
        RuleFor(r => r.Description).NotEmpty().WithMessage(ResourceMessagesExceptions.APPLICATION_DESCRIPTION_EMPTY);
        RuleFor(r => r.Salary).GreaterThan(0).WithMessage(ResourceMessagesExceptions.APPLICATION_SALARY_INVALID);
        RuleFor(r => r.WorkMode).IsInEnum().WithMessage(ResourceMessagesExceptions.APPLICATION_WORKMODE_INVALID);
        RuleFor(r => r.Url).NotEmpty().WithMessage(ResourceMessagesExceptions.URL_EMPTY);
        When(r => r.Url.NotEmpty(), () => {
            RuleFor(x => x.Url).Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage(ResourceMessagesExceptions.URL_INVALID);
        });
        RuleFor(r => r.ApplicationDate).NotEmpty().WithMessage(ResourceMessagesExceptions.APPLICATION_DATE_EMPTY);
        RuleFor(r => r.Status).IsInEnum().WithMessage(ResourceMessagesExceptions.APPLICATION_STATUS_INVALID);
    }
}
