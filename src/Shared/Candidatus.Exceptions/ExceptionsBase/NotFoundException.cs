
using System.Net;

namespace Candidatus.Exceptions.ExceptionsBase;
public class NotFoundException : CandidatusException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
}
