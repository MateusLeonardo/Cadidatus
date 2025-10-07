using Candidatus.Domain.Entities;
using Candidatus.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositories;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository = new();

    public void GetByEmail(User user)
    {
        _repository.Setup(repo => repo.GetByEmail(user.Email)).ReturnsAsync(user);
    }

    public void ExistUserWithEmail(string email)
    {
        _repository.Setup(repo => repo.ExistUserWithEmail(email)).ReturnsAsync(true);
    }

    public IUserReadOnlyRepository Build()
    {
        return _repository.Object;
    }
}