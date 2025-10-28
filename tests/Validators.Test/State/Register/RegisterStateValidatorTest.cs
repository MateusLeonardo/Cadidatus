using Candidatus.Application.UseCases.State.Register;
using Candidatus.Exceptions;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Test.State;
public class RegisterStateValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = RequestRegisterStateJsonBuilder.Build();

        var validator = new RegisterStateValidator();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_StateName_Empty()
    {
        var validator = new RegisterStateValidator();
        var request = RequestRegisterStateJsonBuilder.Build();
        request.Name = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(err => err.ErrorMessage.Equals(ResourceMessagesExceptions.STATE_EMPTY));
    }

    [Fact]
    public void Error_Uf_Empty()
    {
        var request = RequestRegisterStateJsonBuilder.Build();
        request.Uf = string.Empty;

        var result = new RegisterStateValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(err => err.ErrorMessage.Equals(ResourceMessagesExceptions.UF_EMPTY));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public void Error_Uf_Length(int ufLength)
    {
        var request = RequestRegisterStateJsonBuilder.BuildWithUfLength(ufLength);

        var result = new RegisterStateValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(err => err.ErrorMessage.Equals(ResourceMessagesExceptions.UF_LENGTH));
    }
}
