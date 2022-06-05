using Excelia.exam.Application.CQRS.Commands.CreateBeer;
using Excelia.exam.Application.CQRS.Commands.GetBeers;
using Excelia.exam.Application.CQRS.Commands.SearchBeer;
using Exelia.exam.Business.CQRS.Commands.UpdateBeer;
using Excelia.exam.Application.CQRS.DTO;
using Exelia.exam.Business.CQRS.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Exelia.exam.Api.Controllers
{

    [ExcludeFromCodeCoverage]
    [Route("api/beer")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BeerController(IMediator mediator)
        {
            _mediator = mediator;
        }
/// <summary>
/// This end point is to create new beer
/// </summary>
/// <param name="command"></param>
/// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateBeerResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateBeerCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response.Success)
            {
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            return StatusCode(StatusCodes.Status201Created, response);

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetBeersResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<GetBeersResponse> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            GetBeersCommand command = new(pageSize, pageNumber);
            return await _mediator.Send(command);
        }

        [HttpGet("search{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchBeerResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<SearchBeerResponse> Search([FromRoute] string name)
        {
            SearchBeerCommand command = new(name);
            return await _mediator.Send(command);
        }

        [HttpPost("{beerId}rate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddBeerRatingResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<AddBeerRatingResponse> Rate([FromRoute]int beerId, decimal rate)
        {
            AddBeerRatingCommand command = new(rate, beerId);
            return await _mediator.Send(command);
        }
    }
}
