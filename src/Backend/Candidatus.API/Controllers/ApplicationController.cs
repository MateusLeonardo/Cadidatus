using Candidatus.API.Attributes;
using Candidatus.Application.UseCases.Application.Delete;
using Candidatus.Application.UseCases.Application.FindAll;
using Candidatus.Application.UseCases.Application.Register;
using Candidatus.Application.UseCases.Application.Update;
using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Candidatus.API.Controllers;

[AuthenticatedUser]
public class ApplicationController : CandidatusBaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromBody] RequestRegisterApplicationJson request,
        [FromServices] IRegisterApplicationUseCase useCase)
    {
        await useCase.Execute(request);

        return Created(string.Empty, null);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseAllApplicationsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> FindAll([FromServices] IFindAllApplicationUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpPut]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] RequestUpdateApplicationJson request,
        [FromServices] IUpdateApplicationUseCase useCase)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id, [FromServices] IDeleteApplicationUseCase useCase)
    {
        await useCase.Execute(id);

        return NoContent();
    }
}
