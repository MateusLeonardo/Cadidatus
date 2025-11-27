using Candidatus.Communication.Requests;
using Candidatus.Domain.Extensions;
using Candidatus.Exceptions;
using FluentValidation;

namespace Candidatus.Application.UseCases.Platform.Update;

public class UpdatePlatformValidator : AbstractValidator<RequestUpdatePlatformJson>
{
    public UpdatePlatformValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage(ResourceMessagesExceptions.PLATFORM_NAME_EMPTY);
        RuleFor(r => r.Url).NotEmpty().WithMessage(ResourceMessagesExceptions.URL_EMPTY);

        When(r => r.Url.NotEmpty(), () => {
           RuleFor(x => x.Url).Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .When(x => x.Url.NotEmpty()).WithMessage(ResourceMessagesExceptions.URL_INVALID);
        });
    }
}