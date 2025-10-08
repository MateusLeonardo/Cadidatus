using Candidatus.Communication.Responses;
using Candidatus.Domain.Repositories.User;
using Candidatus.Domain.Security.Tokens;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Candidatus.API.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUserReadOnlyRepository _repository;

    public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserReadOnlyRepository repository)
    {
        _accessTokenValidator = accessTokenValidator;
        _repository = repository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);

            var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

            var exists = await _repository.ExistUserWithIdentifier(userIdentifier);

            if (!exists)
                throw new UnauthorizedException(ResourceMessagesExceptions.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson("TokenIsExpired")
            {
                TokenIsExpired = true
            });
        }
        catch (CandidatusException candidatusException)
        {
            context.HttpContext.Response.StatusCode = (int)candidatusException.GetStatusCode();
            context.Result = new ObjectResult(new ResponseErrorJson(candidatusException.GetErrorMessages()));
        }
        catch
        {
            context.Result =
                new UnauthorizedObjectResult(
                    new ResponseErrorJson(ResourceMessagesExceptions.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
        if (string.IsNullOrWhiteSpace(authentication))
            throw new UnauthorizedException(ResourceMessagesExceptions.NO_TOKEN);

        return authentication["Bearer ".Length..].Trim();
    }
}