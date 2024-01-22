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
            var bgImage = await _context.Images.Where(x => x.ImageTypeId == 2 && x.IsSelected == true).Select(x => new ImageModel() { ImageLocation = x.ImageLocation}).FirstOrDefaultAsync();
            return new ServiceResponse<dynamic>()
            {
                Data = bgImage,
            };
        }
        public async Task<ServiceResponse<dynamic>> GetPartnerInfo()
        {
            PartnerModel model = new PartnerModel();
            var partnerInfo = await _context.Partners.Include(x => x.Images).Select(x => new PartnerModel
            {
                PartnerName = x.PartnerName,
                PartnerDescription = x.PartnerDescription,
                PartnerImageId = x.PartnerImageId,
            }).ToListAsync();
            var partnerImage = await _context.Images.Where(x => x.ImageId == partnerInfo.PartnerImageId).Select(x => x.ImageLocation).FirstOrDefaultAsync();
            model = partnerInfo;
            model.PartnerImageLocation = partnerImage;
            return new ServiceResponse<dynamic>()
            {
                Data = model,
            };
        }
    }
}
