using CityFilms.Models;
using CityFilms.Models.Response;

namespace CityFilms.Services.Api.Control.EmailServices
{
    public interface IEmailSender
    {
        Task<ServiceResponse<dynamic>> SendEmail(ContactLogModel model);
    }
}
