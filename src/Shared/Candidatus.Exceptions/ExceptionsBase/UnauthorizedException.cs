using System.Net;

namespace Candidatus.Exceptions.ExceptionsBase;

public class UnauthorizedException : CandidatusException
{
    public UnauthorizedException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages()
    {
        return [Message];
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.Unauthorized;
    }
}