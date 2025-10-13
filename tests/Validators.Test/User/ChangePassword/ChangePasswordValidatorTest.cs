using Candidatus.Application.UseCases.User.ChangePassword;
using Candidatus.Exceptions;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Test.User.ChangePassword;
public class ChangePasswordValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = RequestChangePasswordBuilder.Build();
        var result = new ChangePasswordUserValidator().Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Password_Empty()
    {
        var request = RequestChangePasswordBuilder.Build();
        request.NewPassword = string.Empty;

        var validator = new ChangePasswordUserValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.PASSWORD_EMPTY));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Password_Min_Lenght(int passwordLenght)
    {
        var request = RequestChangePasswordBuilder.Build(passwordLenght);

        var validator = new ChangePasswordUserValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(e => e.ErrorMessage.Equals(ResourceMessagesExceptions.PASSWORD_MIN_LENGTH));
    }
}
