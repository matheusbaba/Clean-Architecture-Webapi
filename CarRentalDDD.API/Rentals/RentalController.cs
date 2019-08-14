using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalDDD.API.Rentals.Commands;
using CarRentalDDD.API.Rentals.Requests;
using CarRentalDDD.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalDDD.API.Rentals
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RentalController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RentalDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] Guid? carId, [FromQuery]Guid? customerId)
        {
            IEnumerable<RentalDTO> result = await _mediator.Send(new RentalByFiltersQuery(carId,customerId));
            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(RentalDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateRentalRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var command = new CreateRentalCommand(request.PickUpDate, request.DropOffDate, request.CustomerId, request.CarId);
                RentalDTO rental = await _mediator.Send(command);
                return Created(string.Empty, rental);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}