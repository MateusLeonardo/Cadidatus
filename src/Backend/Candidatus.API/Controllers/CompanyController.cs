using Candidatus.API.Attributes;
using Candidatus.Application.UseCases.Company.Delete;
using Candidatus.Application.UseCases.Company.FindAll;
using Candidatus.Application.UseCases.Company.Register;
using Candidatus.Application.UseCases.Company.Update;
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

    [HttpGet]
    [ProducesResponseType(typeof(ResponseAllCompanyJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> FindAll([FromServices] IFindAllCompanyUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromRoute] int id, 
        [FromBody] RequestUpdateCompanyJson request, 
        [FromServices] IUpdateCompanyUseCase useCase)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] int id, 
        [FromServices] IDeleteCompanyUseCase useCase)
    {
        await useCase.Execute(id);

        return NoContent();
    }
}
