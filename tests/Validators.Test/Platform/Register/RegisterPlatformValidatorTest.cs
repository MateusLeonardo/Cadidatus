using Candidatus.Application.UseCases.Platform.Register;
using Candidatus.Exceptions;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Test.Platform.Register;

public class RegisterPlatformValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = RequestRegisterPlatformJsonBuilder.Build();

        var validator = new RegisterPlatformValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Name_Empty()
    {
        var request = RequestRegisterPlatformJsonBuilder.Build();
        request.Name = string.Empty;

        var validator = new RegisterPlatformValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(err => err.ErrorMessage.Contains(ResourceMessagesExceptions.PLATFORM_NAME_EMPTY));
    }

    [Fact]
    public void Error_Url_Invalid()
    {
        var request = RequestRegisterPlatformJsonBuilder.Build();
        request.Url = "invalid-url";

        var validator = new RegisterPlatformValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(err => err.ErrorMessage.Contains(ResourceMessagesExceptions.URL_INVALID));
    }
}