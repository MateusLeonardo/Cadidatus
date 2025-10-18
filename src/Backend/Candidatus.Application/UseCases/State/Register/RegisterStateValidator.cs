using Candidatus.Communication.Requests;
using Candidatus.Domain.Extensions;
using Candidatus.Exceptions;
using FluentValidation;

namespace Candidatus.Application.UseCases.State.Register;
public class RegisterStateValidator : AbstractValidator<RequestRegisterStateJson>
{
    public RegisterStateValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage(ResourceMessagesExceptions.STATE_EMPTY);
        RuleFor(r => r.Uf).NotEmpty().WithMessage(ResourceMessagesExceptions.UF_EMPTY);
        When(r => r.Uf.NotEmpty(), () =>
        {
            RuleFor(r => r.Uf).Length(2).WithMessage(ResourceMessagesExceptions.UF_LENGTH);
        });
    }
}
