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
        public async Task<IActionResult> AddNewProperty([FromBody] NewPropertyRequest newPropertyRequest)
        {
            var isSuccessful = await _mediatrSender.Send(new CreatePropertyRequest(newPropertyRequest));
            if (isSuccessful)
            {
                return Ok("Property created successfully");
            }

            return BadRequest("Failed to create property");
        }
    }
}
