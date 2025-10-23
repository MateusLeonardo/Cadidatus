using Candidatus.API.Attributes;
using Candidatus.Application.UseCases.State.FindAll;
using Candidatus.Application.UseCases.State.Register;
using Candidatus.Application.UseCases.State.Update;
using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Candidatus.API.Controllers;

public class StateController : CandidatusBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredStateJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> Register(
        [FromBody] RequestRegisterStateJson request,
        [FromServices] IRegisterStateUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseAllStateJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> FindAll([FromServices] IFindAllStateUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpPut]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [AuthenticatedUser]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] RequestUpdateStateJson request,
        [FromServices] IUpdateStateUseCase useCase)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }
}
