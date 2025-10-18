using Candidatus.API.Attributes;
using Candidatus.Application.UseCases.State.Register;
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
}
