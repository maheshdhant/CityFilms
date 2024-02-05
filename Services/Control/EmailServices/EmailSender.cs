using CityFilms.Models;
using CityFilms.Models.Response;
using System.Net;
using System.Net.Mail;

namespace CityFilms.Services.Control.EmailServices
{
    public class EmailSender : IEmailSender
    {
        public async Task<ServiceResponse<dynamic>> SendEmail(EmailModel model)
        {
            var mail = "mahessdhant@gmail.com";
            var pw = "tsbr ksoe qzno ugol";
            var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(mail, pw),
                EnableSsl = true,
            };
            client.Send(mail, model.To, model.Subject, model.Body);
            return new ServiceResponse<dynamic>()
            {
                Data = "Booking Success!!"
            };
        }
    }
}
