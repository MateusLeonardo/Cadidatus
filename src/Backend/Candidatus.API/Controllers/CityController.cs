using Candidatus.API.Attributes;
using Candidatus.Application.UseCases.City.FindAll;
using Candidatus.Application.UseCases.City.FindOne;
using Candidatus.Application.UseCases.City.Register;
using Candidatus.Application.UseCases.City.Update;
using Candidatus.Communication.Requests;
using Candidatus.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Candidatus.API.Controllers
{
    [AuthenticatedUser]
    public class CityController : CandidatusBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredCityJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register
        (
            [FromBody] RequestRegisterCityJson request,
            [FromServices] IRegisterCityUseCase useCase
        )
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseAllCityJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FindAll([FromServices] IFindAllCitiyUseCase useCase)
        {
            var response = await useCase.Execute();
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseCityJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindOne([FromRoute] int id, [FromServices] IFindOneCityUseCase useCase)
        {
            var response = await useCase.Execute(id);
            
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            [FromRoute] int id, 
            [FromBody] RequestUpdateCityJson request, 
            [FromServices] IUpdateCityUseCase useCase)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }
    }
}
