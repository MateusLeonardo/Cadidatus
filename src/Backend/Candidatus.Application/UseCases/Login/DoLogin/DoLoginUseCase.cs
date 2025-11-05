using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;
using Candidatus.Domain.Extensions;
using Candidatus.Domain.Repositories.User;
using Candidatus.Domain.Security.Cryptography;
using Candidatus.Domain.Security.Tokens;
using Candidatus.Exceptions.ExceptionsBase;
using MapsterMapper;

namespace Candidatus.Application.UseCases.Login.DoLogin;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IMapper _mapper;
    public DoLoginUseCase(IUserReadOnlyRepository userReadOnly,
        IAccessTokenGenerator accessToken,
        IPasswordEncripter passwordEncripter,
        IMapper mapper)
    {
        _userReadOnlyRepository = userReadOnly;
        _accessTokenGenerator = accessToken;
        _passwordEncripter = passwordEncripter;
        _mapper = mapper;
    }
    public async Task<ResponseLoginJson> Execute(RequestLoginJson request)
    {
        var user = await _userReadOnlyRepository.GetByEmail(request.Email);

        if (user is null || _passwordEncripter.Decrypt(request.Password, user.Password).IsFalse())
        {
            throw new InvalidLoginException();
        }

        return new ResponseLoginJson
        {
            User = _mapper.Map<ResponseUserJson>(user),
            Tokens =
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }
}
