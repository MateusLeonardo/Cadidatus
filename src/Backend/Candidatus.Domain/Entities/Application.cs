using Candidatus.Domain.Enums;

namespace Candidatus.Domain.Entities;

public class Application : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public WorkMode WorkMode { get; set; }
    public string Url { get; set; } = string.Empty;
    public DateTime ApplicationDate { get; set; }
    public ApplicationStatus Status { get; set; }
    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public int PlatformId { get; set; }
    public User User { get; set; } = default!;
    public Company Company { get; set; } = default!;
    public Platform Platform { get; set; } = default!;
}