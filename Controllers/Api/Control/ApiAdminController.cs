using CityFlims.Entity;
using CityFlims.Models;
using CityFlims.Services.Control.AdminServices;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace CityFlims.Controllers.Api.Control
{
    [Route("api/control/admin")]
    [ApiController]
    public class ApiAdminController : ControllerBase
    {
        private IAdminServices _actionServices;
        public ApiAdminController(IAdminServices actionServices)
        {
            _actionServices = actionServices;
        }

        [HttpGet("images-list")]
        public async Task<IActionResult> GetImages()
        {
            var serviceResponse = await _actionServices.GetImages();
            return Ok(serviceResponse);
        }

        [HttpPost("upload-images")]
        public async Task<IActionResult> AddImages([FromForm] ImageModel model)
        {
            var serviceResponse = await _actionServices.UploadImages(model);
            return Ok(serviceResponse);
        }
    }
}
