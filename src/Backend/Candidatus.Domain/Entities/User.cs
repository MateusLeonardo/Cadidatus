namespace Candidatus.Domain.Entities;

public class User : EntityBase
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid UserIdentifier { get; set; } = Guid.NewGuid();
    public IList<Company> Companies { get; set; } = [];
}