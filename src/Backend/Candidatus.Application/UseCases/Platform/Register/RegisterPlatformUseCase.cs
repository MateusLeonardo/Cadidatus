using Candidatus.Communication.Requests;
using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.Platform;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Candidatus.Application.UseCases.Platform.Register;

public class RegisterPlatformUseCase : IRegisterPlatformUseCase
{
    private readonly IPlatformWriteOnlyRepository _writeOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public RegisterPlatformUseCase(
        IPlatformWriteOnlyRepository writeOnlyRepository,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser,
        IMapper mapper)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task Execute(RequestRegisterPlatformJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.User();

        var platform = _mapper.Map<Domain.Entities.Platform>(request);
        platform.UserId = loggedUser.Id;

        await _writeOnlyRepository.Add(platform);

        await _unitOfWork.CommitAsync();
    }

    private static void Validate(RequestRegisterPlatformJson request)
    {
        var validator = new RegisterPlatformValidator();
        var result = validator.Validate(request);

        if(!result.IsValid)
            throw new ErrorOnValidationException([.. result.Errors.Select(err => err.ErrorMessage)]);
    }
}