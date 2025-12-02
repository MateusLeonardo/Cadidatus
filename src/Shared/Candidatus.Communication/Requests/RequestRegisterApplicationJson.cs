using Candidatus.Communication.Enums;

namespace Candidatus.Communication.Requests;

public class RequestRegisterApplicationJson
{
    public string Title { get; set;  } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public WorkMode WorkMode { get; set; }
    public string Url { get; set; } = string.Empty;
    public DateTime ApplicationDate { get; set; }
    public ApplicationStatus Status { get; set; }
    public int CompanyId { get; set; }
    public int PlatformId { get; set; }
}
