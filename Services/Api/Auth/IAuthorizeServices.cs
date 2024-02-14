using CityFilms.Models.Response;
using CityFilms.Models;

namespace CityFilms.Services.Api.Auth
{
    public interface IAuthorizeServices
    {
        Task<ServiceResponse<string>> RegisterUser(NewUserRegisterModel model);
        Task<ServiceResponse<dynamic>> Login(LoginModel model);
    }
}
