using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalDDD.API.Customers.Commands;
using CarRentalDDD.API.Customers.Requests;
using CarRentalDDD.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalDDD.API.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            CustomerDTO customer = await _mediator.Send(new CustomerByIdQuery(id));
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }


        [HttpGet(Name = "CustomerByFilters")]
        [ProducesResponseType(typeof(IEnumerable<CustomerDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] string name, [FromQuery] string driverlicense)
        {
            IEnumerable<CustomerDTO> result = await _mediator.Send(new CustomerByFiltersQuery(name, driverlicense));
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateCustomerRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var command = new CreateCustomerCommand(request.Name, request.DriverLicense, request.DOB, request.Street, request.City, request.ZipCode, request.Email, request.Phone);
                CustomerDTO customer = await _mediator.Send(command);
                return Created(string.Empty, customer);
            }
            catch (OException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
