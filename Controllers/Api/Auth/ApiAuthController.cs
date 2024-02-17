using CityFilms.Models;
using CityFilms.Services.Api.Auth;
using CityFilms.Services.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityFilms.Controllers.Api.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class ApiAuthController: ControllerBase
    {
        public readonly IWebHelper _webHelper;
        private IConfiguration _config;
        private IAuthorizeServices _authorizationService;

        public ApiAuthController(IConfiguration config, IAuthorizeServices authorizationService, IWebHelper webHelper)
        {
            _webHelper = webHelper;
            _authorizationService = authorizationService;
            _config = config;
        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var serviceResponse = await _authorizationService.Login(model);
            return Ok(serviceResponse);
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] NewUserRegisterModel model)
        {
            var serviceResponse = await _authorizationService.RegisterUser(model);
            return Ok(serviceResponse);
        }
        [HttpGet("logout-user")]
        public IActionResult LogoutUser()
        {
            _authorizationService.ClearCookie();
            return Ok(DateTime.UtcNow.AddDays(-1).ToString("ddd, dd MMM yyyy hh:mm:ss") + " UTC");
        }
    }
}
