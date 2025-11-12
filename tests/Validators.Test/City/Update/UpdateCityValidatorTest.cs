using Candidatus.Application.UseCases.City.Update;
using Candidatus.Exceptions;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Test.City.Update;

public class UpdateCityValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = RequestUpdateCityJsonBuilder.Build();
        var validator = new UpdateCityValidator();
        var result = validator.Validate(request);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_City_Name_Empty()
    {
        var request = RequestUpdateCityJsonBuilder.Build();
        request.Name = string.Empty;
        var validator = new UpdateCityValidator();
        var result = validator.Validate(request);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(err => err.ErrorMessage.Equals(ResourceMessagesExceptions.CITY_EMPTY));
    }

    [Fact]
    public void Error_State_Id_Invalid()
    {
        var request = RequestUpdateCityJsonBuilder.Build(0);
        var validator = new UpdateCityValidator();
        var result = validator.Validate(request);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(err => err.ErrorMessage.Equals(ResourceMessagesExceptions.STATE_INVALID));
    }
}