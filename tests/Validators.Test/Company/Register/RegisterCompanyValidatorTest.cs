using Candidatus.Application.UseCases.Company.Register;
using Candidatus.Exceptions;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Test.Company.Register;

public class RegisterCompanyValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = RequestRegisterCompanyJsonBuilder.Build();

        var validator = new RegisterCompanyValidator();

        var result = validator.Validate(request);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Error_Name_Empty()
    {
        var request = RequestRegisterCompanyJsonBuilder.Build();
        request.Name = string.Empty;

        var validator = new RegisterCompanyValidator();
        var result = validator.Validate(request);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(err => err.ErrorMessage.Equals(ResourceMessagesExceptions.COMPANY_EMPTY));
    }

    [Fact]
    public void Error_City_Invalid()
    {
        var request = RequestRegisterCompanyJsonBuilder.Build();
        request.CityId = 0;

        var validator = new RegisterCompanyValidator();
        var result = validator.Validate(request);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(err => err.ErrorMessage.Equals(ResourceMessagesExceptions.CITY_INVALID));
    }
}