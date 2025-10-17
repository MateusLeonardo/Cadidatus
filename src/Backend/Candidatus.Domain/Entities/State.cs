namespace Candidatus.Domain.Entities;
public class State : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Uf { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User User { get; set; } = default!;
}
