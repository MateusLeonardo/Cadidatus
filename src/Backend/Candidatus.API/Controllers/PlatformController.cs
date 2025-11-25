using Candidatus.API.Attributes;
using Candidatus.Application.UseCases.Platform.FindAll;
using Candidatus.Application.UseCases.Platform.Register;
using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Candidatus.API.Controllers;

[AuthenticatedUser]
public class PlatformController : CandidatusBaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RequestRegisterPlatformJson request,
        [FromServices] IRegisterPlatformUseCase useCase)
    {
        await useCase.Execute(request);
        return Created(string.Empty, null);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseAllPlatformJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> FindAll([FromServices] IFindAllPlatformUseCase useCase)
    {
        var response = await useCase.Execute();
        return Ok(response);
    }
}