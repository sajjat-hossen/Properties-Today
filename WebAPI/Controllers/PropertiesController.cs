using Application.Features.Properties.Commands;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly ISender _mediatrSender;

        public PropertiesController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }



        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNewProperty([FromBody] NewProperty newPropertyRequest)
        {
            var isSuccessful = await _mediatrSender.Send(new CreatePropertyRequest(newPropertyRequest));

            if (isSuccessful)
            {
                return Ok("Property created successfully");
            }

            return BadRequest("Failed to create property");
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProperty([FromBody] UpdateProperty updateProperty)
        {
            var isSuccessful = await _mediatrSender.Send(new UpdatePropertyRequest(updateProperty));

            if (isSuccessful)
            {
                return Ok("Property update successfully");
            }

            return NotFound("Property does not exists");
        }
    }
}
