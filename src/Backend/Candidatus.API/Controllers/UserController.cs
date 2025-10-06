using Candidatus.API.Binders;
using Candidatus.Application.UseCases.User.GetById;
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
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetUserByIdUseCase useCase,
        [FromRoute] [ModelBinder(typeof(CandidatusIdBinder))]
        int id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }
}