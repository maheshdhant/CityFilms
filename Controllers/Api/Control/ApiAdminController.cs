using CityFilms.Entity;
using CityFilms.Models;
using CityFilms.Services.Api.Control.AdminServices;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace CityFilms.Controllers.Api.Control
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

        [HttpGet("backgroundImages-list")]
        public async Task<IActionResult> GetBackgroundImages()
        {
            var serviceResponse = await _actionServices.GetBackgroundImages();
            return Ok(serviceResponse);
        }

        [HttpPost("upload-images")]
        public async Task<IActionResult> AddImages([FromForm] ImageModel model)
        {
            var serviceResponse = await _actionServices.UploadImages(model);
            return Ok(serviceResponse);
        }

        [HttpPost("select-bg-image/{Id}")]
        public async Task<IActionResult> SelectBackgroundImage(int Id)
        {
            var serviceResponse = await _actionServices.SelectBackgroundImage(Id);
            return Ok(serviceResponse);
        }

        [HttpPost("delete-bg-image/{Id}")]
        public async Task<IActionResult> DeleteBackgroundImage(int Id)
        {
            var serviceResponse = await _actionServices.DeleteBackgroundImage(Id);
            return Ok(serviceResponse);
        }

        [HttpPost("add-partner-info")]
        public async Task<IActionResult> AddPartnerInfo([FromForm]PartnerModel model)
        {
            var serviceResponse = await _actionServices.AddPartnerInfo(model);
            return Ok(serviceResponse);
        }

        [HttpGet("get-partner-info")]
        public async Task<IActionResult> GetPartnerInfo()
        {
            var serviceResponse = await _actionServices.GetPartnerInfo();
            return Ok(serviceResponse);
        }

        [HttpPost("edit-partner-info")]
        public async Task<IActionResult> EditPartnerInfo([FromForm] PartnerModel model)
        {
            var serviceResponse = await _actionServices.EditPartnerInfo(model);
            return Ok(serviceResponse);
        }

        [HttpGet("get-company-profile")]
        public async Task<IActionResult> GetCompanyProfile()
        {
            var serviceResponse = await _actionServices.GetCompanyProfile();
            return Ok(serviceResponse);
        }

        [HttpPost("edit-company-profile")]
        public async Task<IActionResult> EditCompanyProfile([FromBody] CompanyProfileModel model)
        {
            var serviceResponse = await _actionServices.EditCompanyProfile(model);
            return Ok(serviceResponse);
        }
    }
}
