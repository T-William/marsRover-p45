using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using MarsRover.API.Dtos;
using MarsRover.API.Helpers;
using MarsRover.API.Library.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace MarsRover.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoverMovementController : ControllerBase
    {
        private readonly IRoverMovementService _service;
        public RoverMovementController(IRoverMovementService service)
        {
            _service = service;
        }



        [HttpGet("full")]
        public async Task<IActionResult> getRoverMovementsFull(int roverId)
        {

            var roverMovements = await _service.GetRoverMovements(roverId);

            return Ok(roverMovements);
        }
        [HttpGet("{id}", Name = "GetRoverMovement")]
        public async Task<IActionResult> GetRoverMovement(int roverId)
        {

            var roverMovement = await _service.GetRoverMovement(roverId);
            return Ok(roverMovement);


        }

        [HttpPost()]
        public async Task<IActionResult> Create(RoverMovementDto model)
        {

            var validation = await _service.Create(model);
            if (validation.IsValid)
            {
                return CreatedAtRoute("GetRoverMovement", new { controller = "RoverMovement", id = model.Id }, model);
            }
            else
            {
                return BadRequest(validation.Errors);
            }
        }

        [HttpPut("calculate/{gridid}")]
        public async Task<IActionResult> CalculateMovement(int gridId,RoverMovementDto model )
        {
            var validation = await _service.CalculateMovement(gridId,model);
            if(validation.IsValid)
            {
                return NoContent();                
            }
            else{
                return BadRequest(validation.Errors);
            }
        }

    }
}