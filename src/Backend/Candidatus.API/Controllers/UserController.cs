using Candidatus.API.Attributes;
using Candidatus.Application.UseCases.User.ChangePassword;
using Candidatus.Application.UseCases.User.Profile;
using Candidatus.Application.UseCases.User.Register;
using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Candidatus.API.Controllers;

public class UserController : CandidatusBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RequestRegisterUserJson request,
        [FromServices] IRegisterUserUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> GetUserProfile([FromServices] IGetUserProfileUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpPut]
    [Route("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    [AuthenticatedUser]
    public async Task<IActionResult> ChangePassword(
        [FromBody] RequestChangePasswordUserJson request,
        [FromServices] IChangePasswordUserUseCase useCase)
    {
        await useCase.Execute(request);

        return NoContent();
    }
}