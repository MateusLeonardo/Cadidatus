using Candidatus.Communication.Requests;
using Candidatus.Domain.Extensions;
using Candidatus.Exceptions;
using FluentValidation;

namespace Candidatus.Application.UseCases.User.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_EMPTY);
        When(user => user.Email.NotEmpty(), () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesExceptions.EMAIL_INVALID);
        });

        RuleFor(user => user.Password).NotEmpty().WithMessage(ResourceMessagesExceptions.PASSWORD_EMPTY);
        When(user => user.Password.NotEmpty(), () =>
        {
            RuleFor(user => user.Password).MinimumLength(6).WithMessage(ResourceMessagesExceptions.PASSWORD_MIN_LENGTH);
        });
    }
}
