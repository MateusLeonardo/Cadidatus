using Candidatus.Communication.Requests;
using Candidatus.Domain.Extensions;
using Candidatus.Domain.Repositories;
using Candidatus.Domain.Repositories.User;
using Candidatus.Domain.Security.Cryptography;
using Candidatus.Domain.Services.LoggedUser;
using Candidatus.Exceptions;
using Candidatus.Exceptions.ExceptionsBase;

namespace Candidatus.Application.UseCases.User.ChangePassword;
public class ChangePasswordUserUseCase : IChangePasswordUserUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
    private readonly IPasswordEncripter _passwordEncripter;

    public ChangePasswordUserUseCase(
        IUserUpdateOnlyRepository updateRepository,
        IUnitOfWork unitOfWork,
        IPasswordEncripter passwordEncripter,
        ILoggedUser loggedUser)
    {
        _passwordEncripter = passwordEncripter;
        _unitOfWork = unitOfWork;
        _userUpdateOnlyRepository = updateRepository;
        _loggedUser = loggedUser;
    }

    public async Task Execute(RequestChangePasswordUserJson request)
    {
        var loggedUser = await _loggedUser.User();

        Validate(request, loggedUser);

        var user = await _userUpdateOnlyRepository.GetById(loggedUser.Id);

        user!.Password = _passwordEncripter.Encrypt(request.NewPassword);

        _userUpdateOnlyRepository.Update(user);

        await _unitOfWork.CommitAsync();
    }

    private void Validate(RequestChangePasswordUserJson request, Domain.Entities.User loggedUser)
    {
        var result = new ChangePasswordUserValidator().Validate(request);

        if (_passwordEncripter.Decrypt(request.Password, loggedUser.Password).IsFalse())
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesExceptions.PASSWORD_DIFFERENT_CURRENT_PASSWORD));

        if (!result.IsValid)
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
    }
}
