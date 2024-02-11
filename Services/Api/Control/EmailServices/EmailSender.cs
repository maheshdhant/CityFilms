using CityFilms.Entity;
using CityFilms.Models;
using CityFilms.Models.Response;
using System.Net;
using System.Net.Mail;

namespace CityFilms.Services.Api.Control.EmailServices
{
    public class EmailSender : IEmailSender
    {
        public async Task<ServiceResponse<dynamic>> SendEmail(ContactLogModel model)
        {
            using var ent = new CityfilmsDataContext();

            var sender = "mahessdhant@gmail.com";
            var pw = "tsbr ksoe qzno ugol";

            var mailLogObj = new MailLog()
            {
                SentTo = model.Email,
                SentBy = sender,
                Subject = model.Subject,
                CreatedDate = DateTime.Now,
            };
            await ent.AddAsync(mailLogObj);
            var contactLogObj = new ContactLog()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Message = model.Message,
                CreatedDate = DateTime.UtcNow,
            };
            await ent.AddAsync(contactLogObj);
            await ent.SaveChangesAsync();
            var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(sender, pw),
                EnableSsl = true,
            };
            client.Send(sender, model.Email, model.Subject, model.Message);
            return new ServiceResponse<dynamic>()
            {
                Data = "Booking Success!!"
            };
        }
    }
}
