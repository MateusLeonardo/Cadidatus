using Candidatus.Communication.Requests;
using Candidatus.Exceptions;
using FluentValidation;

namespace Candidatus.Application.UseCases.User.Register;
internal class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesExceptions.EMAIL_EMPTY);
    }
}
