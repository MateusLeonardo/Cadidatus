using Candidatus.Communication.Responses;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Candidatus.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CandidatusException candidatusException)
        {
            HandleCandidatusException(context, candidatusException);
        }
        else
        {
            ThrowUnknowException(context);
        }
    }

    private static void HandleCandidatusException(ExceptionContext context, CandidatusException candidatusException)
    {
        context.HttpContext.Response.StatusCode = (int)candidatusException.GetStatusCode();
        context.Result = new ObjectResult(new ResponseErrorJson(candidatusException.GetErrorMessages()));
    }

    private static void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesExceptions.UNKNOW_ERROR));
    }
}
