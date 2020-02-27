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
    public class RoverController : ControllerBase
    {
        private readonly IRoverService _service;
        private readonly IMapper _mapper;

        public RoverController(IRoverService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

        }

        [HttpGet("full/{gridid}")]
        public ActionResult getRovers(int gridId)
        {
            var rovers = _service.GetRovers(gridId);

            return Ok(rovers);


        }
        [HttpGet("{id}", Name = "GetRover")]
        public async Task<IActionResult> getRover(int id)
        {
            var rover = await _service.GetRover(id);
            return Ok(rover);
        }

        [HttpPut("calculate/{gridid}")]
        public async Task<IActionResult> CalculateMovement(int gridId, List<RoverDto> model)
        {
            var validation = await _service.CalculateMovement(gridId, model);
            if (validation.IsValid)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(validation.Errors);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Create(RoverDto model)
        {

            var validation = await _service.Create(model);
            if (validation.IsValid)
            {
                return CreatedAtRoute("GetRover", new { controller = "Rover", id = model.Id }, model);
            }
            else
            {
                return BadRequest(validation.Errors);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RoverDto model)
        {
            var validation = await _service.Update(id, model);
            if (validation.IsValid)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(validation.Errors);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRover(int id)
        {

            var validation = await _service.Delete(id);
            if (validation.IsValid)
            {
                return Ok();
            }
            else
            {
                return BadRequest(validation.Errors);
            }

        }




    }
}