﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalDDD.API.Cars.Commands;
using CarRentalDDD.API.Cars.Requests;
using CarRentalDDD.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalDDD.API.Cars
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CarWithMaintenancesDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            CarWithMaintenancesDTO car = await _mediator.Send(new CarByIdQuery(id));
            if (car == null)
                return NotFound();
            return Ok(car);
        }


        [HttpGet(Name = "CarByFilters")]
        [ProducesResponseType(typeof(IEnumerable<CarDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] string make, [FromQuery] string model)
        {
            IEnumerable<CarDTO> result = await _mediator.Send(new CarByFiltersQuery(model, make));
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CarDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateCarRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                CarDTO car = await _mediator.Send(new CreateCarCommand(request.Model, request.Make, request.Registration, request.Odmometer, request.Year));
                return Created(string.Empty, car);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{CarId}/Maintenance", Name = "Maintenance")]
        [ProducesResponseType(typeof(MaintenanceDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromRoute] Guid CarId, [FromBody] CreateMaintenanceRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                MaintenanceDTO maintenance = await _mediator.Send(new CreateMaintenanceCommand(CarId, request.Date, request.Service, request.Description));
                return Created(string.Empty, maintenance);
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}