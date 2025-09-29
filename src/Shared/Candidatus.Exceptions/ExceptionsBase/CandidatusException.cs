using System.Net;

namespace Candidatus.Exceptions.ExceptionsBase;
public abstract class CandidatusException : SystemException
{
    protected CandidatusException(string message) : base(message) { }

    public abstract IList<string> GetErrorMessages();

    public abstract HttpStatusCode GetStatusCode();
}
