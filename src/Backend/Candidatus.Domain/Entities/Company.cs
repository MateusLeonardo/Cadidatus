namespace Candidatus.Domain.Entities;
public class Company : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public int CityId { get; set; }
    public required City City { get; set; }
}
