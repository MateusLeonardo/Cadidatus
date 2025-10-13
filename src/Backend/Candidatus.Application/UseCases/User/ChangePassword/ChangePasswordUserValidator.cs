using Candidatus.Application.SharedValidators;
using Candidatus.Communication.Requests;
using FluentValidation;

namespace Candidatus.Application.UseCases.User.ChangePassword;
public class ChangePasswordUserValidator : AbstractValidator<RequestChangePasswordUserJson>
{
    public ChangePasswordUserValidator()
    {
        RuleFor(r => r.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordUserJson>());
    }
}
