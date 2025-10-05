using Candidatus.Application.UseCases.User.Register;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Test.User.Register;
public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }
}
