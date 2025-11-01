using Candidatus.Application.UseCases.City.Register;
using Candidatus.Exceptions;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Test.City.Register
{
    public class RegisterCityValidatorTest
    {
        [Fact]
        public void Success()
        {
            var request = RequestRegisterCityJsonBuilder.Build(1);

            var validator = new RegisterCityValidator();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_City_Name_Empty(){
            var request = RequestRegisterCityJsonBuilder.Build(1);
            request.Name = string.Empty;

            var validator = new RegisterCityValidator();

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And
                .Contain(err => err.ErrorMessage.Equals(ResourceMessagesExceptions.CITY_EMPTY));
        }
    }
}

