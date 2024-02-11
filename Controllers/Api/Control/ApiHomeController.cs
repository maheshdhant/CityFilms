using CityFilms.Entity;
using CityFilms.Models;
using CityFilms.Services.Api.Control.EmailServices;
using CityFilms.Services.Api.Control.HomeServices;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace CityFilms.Controllers.Api.Control
{
    [Route("api/control/home")]
    [ApiController]
    public class ApiHomeController : ControllerBase
    {
        private IHomeServices _actionServices;
        private IEmailSender _emailSender;
        public ApiHomeController(IHomeServices actionServices, IEmailSender emailSender)
        {
            _actionServices = actionServices;
            _emailSender = emailSender;
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
        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] ContactLogModel model)
        {
            var serviceResponse = await _emailSender.SendEmail(model);
            return Ok(serviceResponse);
        }
    }
}  