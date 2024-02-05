using CityFilms.Models;
using CityFilms.Models.Response;

namespace CityFilms.Services.Control.EmailServices
{
    public interface IEmailSender
    {
        Task<ServiceResponse<dynamic>> SendEmail(EmailModel model);
    }
}
