using Candidatus.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositories;

public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _repository = new();

    public UserReadOnlyRepositoryBuilder GetByEmail(Candidatus.Domain.Entities.User user)
    {
        _repository.Setup(repo => repo.GetByEmail(user.Email)).ReturnsAsync(user);
        return this;
    }

    public UserReadOnlyRepositoryBuilder ExistUserWithEmail(string email)
    {
        _repository.Setup(repo => repo.ExistUserWithEmail(email)).ReturnsAsync(true);
        return this;
    }

    public IUserReadOnlyRepository Build()
    {
        return _repository.Object;
    }
}