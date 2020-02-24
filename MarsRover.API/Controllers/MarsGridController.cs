using System.Threading.Tasks;
using MarsRover.API.Dtos;
using MarsRover.API.Library.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarsRover.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarsGridController : ControllerBase
    {
        private readonly IMarsGridService _service;

        public MarsGridController(IMarsGridService service)
        {
            _service = service;
        }

        [HttpGet("full")]
        public async Task<IActionResult> getMarsGridsFull()
        {

            var grids = await _service.GetMarsGridFull();

            return Ok(grids);


        }

        [HttpGet("{id}", Name = "GetMarsGrid")]
        public async Task<IActionResult> GetMarsGrid(int id)
        {

            var grid = await _service.GetMarsGrid(id);
            return Ok(grid);
        }

        [HttpPost()]
        public async Task<IActionResult> Create(MarsGridDto model)
        {

            var validation = await _service.Create(model);
            if (validation.IsValid)
            {
                return CreatedAtRoute("GetMarsGrid", new { controller = "MarsGrid", id = model.Id }, model);
            }
            else
            {
                return BadRequest(validation.Errors);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MarsGridDto model)
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
        public async Task<IActionResult> DeleteMarsGrid(int id)
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