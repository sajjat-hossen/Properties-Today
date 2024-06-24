using Application.Features.Properties.Commands;
using Application.Features.Properties.Queries;
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
        #region Properties

        private readonly ISender _mediatrSender;

        #endregion

        #region Constructor

        public PropertiesController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }

        #endregion

        #region AddNewProperty

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

        #endregion

        #region UpdateProperty

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

        #endregion

        #region DeleteProperty

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteProperty(int id)
        {
            var isSuccessful = await _mediatrSender.Send(new  DeletePropertyRequest(id));

            if (isSuccessful)
            {
                return Ok("Property deleted successfully");
            }

            return NotFound("Property does not exists");
        }

        #endregion

        #region GetProperty

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProperty(int id)
        {
            var property = await _mediatrSender.Send(new GetPropertyByIdRequest(id));

            if (property != null)
            {
                return Ok(property);
            }

            return NotFound("Property does not exists");
        }

        #endregion
    }
}
