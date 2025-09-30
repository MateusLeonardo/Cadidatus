using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;
using Candidatus.Exceptions.ExceptionsBase;

namespace Candidatus.Application.UseCases.User.Register;
internal class RegisterUserUseCase : IRegisterUserUseCase
{
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        return new ResponseRegisteredUserJson
        {
            Email = request.Email
        };

    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(error => error.ErrorMessage).ToList());

    }
}
