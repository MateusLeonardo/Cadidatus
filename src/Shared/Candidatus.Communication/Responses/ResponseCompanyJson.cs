namespace Candidatus.Communication.Responses;
public class ResponseCompanyJson
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CityId { get; set; }

    public ResponseCityJson? City { get; set; }
}
