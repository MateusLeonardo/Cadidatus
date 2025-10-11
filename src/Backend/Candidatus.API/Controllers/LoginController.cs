using Candidatus.Application.UseCases.Login.DoLogin;
using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Candidatus.API.Controllers;

public class LoginController : CandidatusBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] RequestLoginJson request,
        [FromServices] IDoLoginUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }
}