using CityFlims.Entity;
using CityFlims.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task AddImages(ImageModel image)
        {
            
            await _context.SaveChangesAsync();

            var fileName = image.ImageFile.Name + Path.GetExtension(image.ImageFile.FileName);

            // Define the path to save the uploaded image
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
            var imageDetails = new Image
            {
                ImageLocation = filePath,
                ImageName = fileName,
            };
            _context.Add(imageDetails);
            // Save the image to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.ImageFile.CopyToAsync(stream);
            }
        }
    }
}
