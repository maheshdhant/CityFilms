using CityFilms.Models.Response;
using CityFilms.Models;

namespace CityFilms.Services.Api.Auth
{
    public interface IAuthorizeService
    {
        Task<ServiceResponse<string>> RegisterUser(NewUserRegisterModel model);
        Task<ServiceResponse<dynamic>> Login(LoginModel model);
    }
}
