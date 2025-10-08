using Candidatus.Domain.Entities;

namespace Candidatus.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    public Task<User> User();
}