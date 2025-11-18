namespace Candidatus.Domain.Entities;

public class City : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public int StateId { get; set; }
    public int UserId { get; set; }
    public required State State { get; set; }
    public required User User { get; set; }

    public IList<Company> Companies { get; set; } = [];
}
