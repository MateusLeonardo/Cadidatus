using Candidatus.Domain.Repositories.City;
using Moq;

namespace CommonTestUtilities.Repositories.City;

public class CityReadOnlyRepositoryBuilder
{
    private readonly Mock<ICityReadOnlyRepository> _repository = new(); 

    public CityReadOnlyRepositoryBuilder FindById(Candidatus.Domain.Entities.City city, Candidatus.Domain.Entities.User user)
    {
        _repository.Setup(r => r.FindById(city.Id, user)).ReturnsAsync(city);
        return this;
    }

    public ICityReadOnlyRepository Build() => _repository.Object;
}
