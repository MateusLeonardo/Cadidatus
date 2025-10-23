using Candidatus.Application.UseCases.State.Update;
using Candidatus.Exceptions;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Test.State.Update;

public class UpdateStateValidatorTest
{

    [Fact]
    public void Success()
    {
        var request = RequestUpdateStateJsonBuilder.Build();
        var result = new UpdateStateValidator().Validate(request);

        result.IsValid.Should().BeTrue();
    }


    [Fact]
    public void Error_State_Name_Empty()
    {
        var request = RequestUpdateStateJsonBuilder.Build();
        request.Name = string.Empty;

        var result = new UpdateStateValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(err => err.ErrorMessage.Equals(ResourceMessagesExceptions.STATE_EMPTY));
    }

    [Fact]
    public void Error_State_Uf_Empty()
    {
        var request = RequestUpdateStateJsonBuilder.Build();
        request.Uf = string.Empty;

        var result = new UpdateStateValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(err => err.ErrorMessage.Equals(ResourceMessagesExceptions.UF_EMPTY));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public void Error_State_Uf_Length(int length)
    {
        var request = RequestUpdateStateJsonBuilder.BuildWithUfLength(length);

        var result = new UpdateStateValidator().Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(err => err.ErrorMessage.Equals(ResourceMessagesExceptions.UF_LENGTH));
    }
}
