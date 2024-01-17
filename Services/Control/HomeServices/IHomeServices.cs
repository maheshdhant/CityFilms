using CityFilms.Models;
using CityFilms.Models.Response;

namespace CityFilms.Services.Control.HomeServices
{
    public interface IHomeServices
    {
        Task<ServiceResponse<dynamic>> GetCompanyLogo();
        Task<ServiceResponse<dynamic>> GetCurrentBackground();
    }
}
