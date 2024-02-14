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
        private IAuthorizeService _authorizationService;

        public ApiAuthController(IConfiguration config, IAuthorizeService authorizationService, IWebHelper webHelper)
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
    }
}
