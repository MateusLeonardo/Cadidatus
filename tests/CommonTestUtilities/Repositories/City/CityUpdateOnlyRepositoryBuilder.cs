using Candidatus.Domain.Repositories.City;
using Moq;

namespace CommonTestUtilities.Repositories.City;

public class CityUpdateOnlyRepositoryBuilder
{
    private readonly Mock<ICityUpdateOnlyRepository> _repository = new();

    public CityUpdateOnlyRepositoryBuilder FindById(
        Candidatus.Domain.Entities.User user,
        Candidatus.Domain.Entities.City city)
    {
        _repository.Setup(repo => repo.FindById(city.Id, user)).ReturnsAsync(city);
        return this;
    }

    public CityUpdateOnlyRepositoryBuilder Update(Candidatus.Domain.Entities.City city)
    {
        _repository.Setup(repo => repo.Update(city));
        return this;
    }

    public ICityUpdateOnlyRepository Build() => _repository.Object;
}