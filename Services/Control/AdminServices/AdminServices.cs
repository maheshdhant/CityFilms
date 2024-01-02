using CityFlims.Entity;
using CityFlims.Models;
using Microsoft.EntityFrameworkCore;

namespace CityFlims.Services.Control.AdminServices
{
    public class AdminServices:IAdminServices
    {
        private readonly CityfilmsDataContext _context;
        public AdminServices(CityfilmsDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ImageModel>> GetImages()
        {

            var imageList = await _context.Images.Select(x => new ImageModel()
            {
                ImageName = x.ImageName,
                ImageLocation = x.ImageLocation,
            }).ToListAsync();

            return imageList;
        }

        public void AddImage(ImageModel image)
        {

        }
    }
}
