using Candidatus.Communication.Responses;
using Candidatus.Domain.Repositories.User;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Candidatus.Application.UseCases.User.GetById;

public class GetUserByIdUseCase : IGetUserByIdUseCase
{
    private readonly IMapper _mapper;
    private readonly IUserWriteOnlyRepository _repository;

    public GetUserByIdUseCase(IUserWriteOnlyRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ResponseUserJson> Execute(int id)
    {
        var user = await _repository.GetById(id);

        if (user is null)
            throw new NotFoundException(ResourceMessagesExceptions.USER_NOT_FOUND);

        return _mapper.Map<ResponseUserJson>(user);
    }
}