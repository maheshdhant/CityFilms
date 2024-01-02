using CityFlims.Entity;
using CityFlims.Models;
using CityFlims.Services.Control.AdminServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IEnumerable<ImageModel>> GetImages()
        {
            var serviceResponse = await _actionServices.GetImages();
            return serviceResponse;
        }
    }
}
