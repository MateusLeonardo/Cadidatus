namespace Candidatus.Domain.Entities;

public class Platform : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User User { get; set; } = default!;
}