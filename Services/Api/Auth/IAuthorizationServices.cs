using CityFilms.Models.Response;
using CityFilms.Models;

namespace CityFilms.Services.Api.Auth
{
    public interface IAuthorizationServices
    {
        Task<ServiceResponse<string>> RegisterUser(NewUserRegisterModel model);
    }
}
