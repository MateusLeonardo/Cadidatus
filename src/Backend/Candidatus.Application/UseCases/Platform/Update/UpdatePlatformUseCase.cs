using Candidatus.Communication.Requests;
using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.Platform;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Candidatus.Application.UseCases.Platform.Update;

public class UpdatePlatformUseCase : IUpdatePlatformUseCase
{
    private readonly IPlatformUpdateOnlyRepository _platformUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    public UpdatePlatformUseCase(
        IPlatformUpdateOnlyRepository platformUpdateOnlyRepository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser,
        IMapper mapper)
    {
        _platformUpdateOnlyRepository = platformUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }
    public async Task Execute(int platformId, RequestUpdatePlatformJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.User();

        var platform = await _platformUpdateOnlyRepository.FindById(platformId, loggedUser);

        if(platform is null)
            throw new NotFoundException(ResourceMessagesExceptions.PLATFORM_NOT_FOUND);

       _mapper.Map(request, platform);

        _platformUpdateOnlyRepository.Update(platform);

        await _unitOfWork.CommitAsync();
    }

    private static void Validate(RequestUpdatePlatformJson request)
    {
        var validator = new UpdatePlatformValidator();
        var result = validator.Validate(request);

        if(!result.IsValid)
            throw new ErrorOnValidationException([.. result.Errors.Select(err => err.ErrorMessage)]);
    }
}