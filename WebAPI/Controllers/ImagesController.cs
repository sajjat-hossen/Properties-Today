using Application.Features.Images.Commands;
using Application.Features.Images.Queries;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        #region Properties

        private readonly ISender _mediatrSender;

        #endregion

        #region Constructor

        public ImagesController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }

        #endregion

        #region AddNewImage

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> AddNewImage(NewImage newImage)
        {
            var isSuccessful = await _mediatrSender.Send(new CreateImageRequest(newImage));

            if (isSuccessful)
            {
                return Ok("Image created successfully");
            }

            return BadRequest("Failed to create image");
        }

        #endregion

        #region UpdateImage

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> UpdateImage([FromBody] UpdateImage updateImage)
        {
            var isSuccessfull = await _mediatrSender.Send(new UpdateImageRequest(updateImage));

            if (isSuccessfull)
            {
                return Ok("Image updated successfully");
            }

            return NotFound("Image does not exists");
        }

        #endregion

        #region DeleteImage

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeleteImage(int id)
        {
            var isSuccesful = await _mediatrSender.Send(new DeleteImageRequest(id));

            if (isSuccesful)
            {
                return Ok("Image deleted successfully");
            }

            return NotFound("Image does not exists");
        }

        #endregion

        #region GetImage

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _mediatrSender.Send(new GetImageByIdRequest(id));

            if (image != null)
            {
                return Ok(image);
            }

            return NotFound("Image does not exists");
        }

        #endregion

        #region

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetImages()
        {
            var images = await _mediatrSender.Send(new GetImagesRequest());

            if (images != null)
            {
                return Ok(images);
            }

            return NotFound("No images found");
        }

        #endregion
    }
}
