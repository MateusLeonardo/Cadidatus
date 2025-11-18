using Candidatus.API.Attributes;
using Candidatus.Application.UseCases.Company.Register;
using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Candidatus.API.Controllers;

[AuthenticatedUser]
public class CompanyController : CandidatusBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredCompanyJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
            [FromBody] RequestRegisterCompanyJson request,
            [FromServices] IRegisterCompanyUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
