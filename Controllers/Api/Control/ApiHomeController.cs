using CityFilms.Entity;
using CityFilms.Models;
using CityFilms.Services.Control.HomeServices;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace CityFilms.Controllers.Api.Control
{
    [Route("api/control/home")]
    [ApiController]
    public class ApiHomeController : ControllerBase
    {
        private IHomeServices _actionServices;
        public ApiHomeController(IHomeServices actionServices)
        {
            _actionServices = actionServices;
        }
        [HttpGet("company-logo")]
        public async Task<IActionResult> GetCompanyLogo()
        {
            var serviceResponse = await _actionServices.GetCompanyLogo();
            return Ok(serviceResponse);
        }

        [HttpGet("current-bg")]
        public async Task<IActionResult> GetCurrentBackground()
        {
            var serviceResponse = await _actionServices.GetCurrentBackground();
            return Ok(serviceResponse);
        }
        [HttpGet("partner-info")]
        public async Task<IActionResult> GetPartnerInfo()
        {
            var serviceResponse = await _actionServices.GetPartnerInfo();
            return Ok(serviceResponse);
        }
    }
}  