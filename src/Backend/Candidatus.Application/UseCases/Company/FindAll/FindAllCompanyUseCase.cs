using Candidatus.Communication.Responses;
using Candidatus.Domain.Repositories.Company;
using Candidatus.Domain.Services.LoggedUser;
using MapsterMapper;

namespace Candidatus.Application.UseCases.Company.FindAll;

public class FindAllCompanyUseCase : IFindAllCompanyUseCase
{
    private readonly ICompanyReadOnlyRepository _companyReadOnlyRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public FindAllCompanyUseCase(
        ICompanyReadOnlyRepository companyReadOnlyRepository,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _companyReadOnlyRepository = companyReadOnlyRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseAllCompanyJson> Execute()
    {
        var user = await _loggedUser.User();
        var companies = await _companyReadOnlyRepository.FindAll(user);
        
        return _mapper.Map<ResponseAllCompanyJson>(companies);
    }
}