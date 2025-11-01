namespace Candidatus.Communication.Responses;

public class ResponseCityJson
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int StateId { get; set; }
    public ResponseStateJson State { get; set; } = new();
}