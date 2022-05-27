using Microsoft.AspNetCore.Mvc;
using MediatR;
using Excelia.exam.Application.CQRS.Commands.CreateBeer;

namespace Exelia.exam.Api.Controllers
{
    [Route("api/beer")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BeerController(IMediator meditator)
        {
            _mediator = meditator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBeerCommand command)
        {
            var response= await _mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, response);

        }
    }
}
