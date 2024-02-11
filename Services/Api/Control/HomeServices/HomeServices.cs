using CityFilms.Entity;
using CityFilms.Models;
using CityFilms.Models.Response;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

namespace CityFilms.Services.Api.Control.HomeServices
{
    public class HomeServices : IHomeServices
    {
        private readonly CityfilmsDataContext _context;
        private readonly EmailConfig _mailSettings;
        public HomeServices(CityfilmsDataContext context, IOptions<EmailConfig> mailSettings)
        {
            _context = context;
            _mailSettings = mailSettings.Value;
        }
        public async Task<ServiceResponse<dynamic>> GetCompanyLogo()
        {
            var logoLocation = "../uploads/images/logo/logo.jpg";

            return new ServiceResponse<dynamic>()
            {
                Data = logoLocation
            };
        }
        public async Task<ServiceResponse<dynamic>> GetCurrentBackground()
        {
            var bgImage = await _context.Images.Where(x => x.ImageTypeId == 2 && x.IsSelected == true).Select(x => new ImageModel() { ImageName = x.ImageName }).FirstOrDefaultAsync();
            var imageLocation = "../uploads/images/background/" + bgImage.ImageName;
            return new ServiceResponse<dynamic>()
            {
                Data = imageLocation,
            };
        }
        public async Task<ServiceResponse<dynamic>> GetPartnerInfo()
        {
            using var ent = new CityfilmsDataContext();
            List<PartnerModel> model = new List<PartnerModel>();
            var partnerInfo = await ent.Partners.Include(x => x.PartnerImage).Select(x => new PartnerModel
            {
                PartnerName = x.PartnerName,
                PartnerDescription = x.ParnterDescription,
                PartnerWebsite = x.PartnerWebsite,
                PartnerImageId = x.PartnerImageId,
                PartnerImageLocation = x.PartnerImage.ImageLocation,
            }).ToListAsync();
            model = partnerInfo;
            return new ServiceResponse<dynamic>()
            {
                Data = model,
            };
        }
        public async Task<bool> EmailSender(EmailModel model)
        {
            model.Sender = _mailSettings.Email;
            model.Password = _mailSettings.Password;
            var a = true;
            using (MailMessage mm = new MailMessage(model.Sender, model.To))
            {
                mm.Subject = model.Subject;
                mm.Body = model.Body;
                if (model.Attachment != null && model.Attachment.Length > 0)
                {
                    string fileName = Path.GetFileName(model.Attachment.FileName);
                    mm.Attachments.Add(new Attachment(model.Attachment.OpenReadStream(), fileName));
                }
                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = _mailSettings.Host;
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(model.Sender, model.Password);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = _mailSettings.Port;
                    smtp.Send(mm);
                }
            }
            return true;
        }
    }
}
