namespace Candidatus.Communication.Responses;
public class ResponseLoginJson
{
   public ResponseUserJson User { get; set; } = new();
    public ResponseTokensJson Tokens { get; set; } = new();
}
