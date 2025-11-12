using Candidatus.Domain.Repositories.City;
using Moq;

namespace CommonTestUtilities.Repositories.City;

public class CityUpdateOnlyRepositoryBuilder
{
    private readonly Mock<ICityUpdateOnlyRepository> _repository = new();

    public void FindById(
        Candidatus.Domain.Entities.User user,
        Candidatus.Domain.Entities.City city)
    {
        _repository.Setup(repo => repo.FindById(city.Id, user)).ReturnsAsync(city);
    }

    public void Update(Candidatus.Domain.Entities.City city)
    {
        _repository.Setup(repo => repo.Update(city));
    }

    public ICityUpdateOnlyRepository Build() => _repository.Object;
}