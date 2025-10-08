using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;
using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.User;
using Candidatus.Domain.Security.Cryptography;
using Candidatus.Domain.Security.Tokens;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;
using FluentValidation.Results;
using MapsterMapper;

namespace Candidatus.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;

    public RegisterUserUseCase(
        IUserWriteOnlyRepository writeOnlyRepository,
        IUserReadOnlyRepository readOnlyRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _userWriteOnlyRepository = writeOnlyRepository;
        _userReadOnlyRepository = readOnlyRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);

        await _userWriteOnlyRepository.Add(user);

        await _unitOfWork.CommitAsync();

        var response = _mapper.Map<ResponseRegisteredUserJson>(user);
        response.Tokens = new ResponseTokensJson
        {
            AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
        };

        return response;
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = await validator.ValidateAsync(request);
        var user = await _userReadOnlyRepository.ExistUserWithEmail(request.Email);

        if (user)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesExceptions.EMAIL_EXISTS));

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(error => error.ErrorMessage).ToList());
    }
}