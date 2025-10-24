using Candidatus.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositories.User;
public class UserUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IUserUpdateOnlyRepository> _repository;

    public UserUpdateOnlyRepositoryBuilder() => _repository = new Mock<IUserUpdateOnlyRepository>();

    public UserUpdateOnlyRepositoryBuilder GetById(Candidatus.Domain.Entities.User user)
    {
        _repository.Setup(repo => repo.GetById(user.Id)).ReturnsAsync(user);
        return this;
    }

    public IUserUpdateOnlyRepository Build() => _repository.Object;
}
