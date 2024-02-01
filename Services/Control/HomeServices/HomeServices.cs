using CityFilms.Entity;
using CityFilms.Models;
using CityFilms.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace CityFilms.Services.Control.HomeServices
{
    public class HomeServices: IHomeServices
    {
        private readonly CityfilmsDataContext _context;
        public HomeServices(CityfilmsDataContext context)
        {
            _context = context;
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
            var bgImage = await _context.Images.Where(x => x.ImageTypeId == 2 && x.IsSelected == true).Select(x => new ImageModel() { ImageName = x.ImageName}).FirstOrDefaultAsync();
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
    }
}
