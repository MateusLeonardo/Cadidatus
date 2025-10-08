using Candidatus.Communication.Responses;
using Candidatus.Domain.Repositories.User;
using Candidatus.Domain.Services.LoggedUser;
using MapsterMapper;

namespace Candidatus.Application.UseCases.User.Profile;

public class GetUserProfileUseCase : IGetUserProfileUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    private readonly IUserWriteOnlyRepository _repository;

    public GetUserProfileUseCase(IUserWriteOnlyRepository repository, IMapper mapper, ILoggedUser loggedUser)
    {
        _mapper = mapper;
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseUserJson> Execute()
    {
        var user = await _loggedUser.User();

        return _mapper.Map<ResponseUserJson>(user);
    }
}