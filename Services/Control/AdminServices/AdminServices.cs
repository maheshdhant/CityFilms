using CityFlims.Entity;
using CityFlims.Models;
using CityFlims.Models.Response;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<ServiceResponse<dynamic>> GetImages()
        {

            var imageList = await _context.Images.Select(x => new ImageModel()
            {
                ImageName = x.ImageName,
                ImageLocation = x.ImageLocation,
            }).ToListAsync();

            return new ServiceResponse<dynamic>()
            {
                Data = imageList
            };
        }

        public async Task<ServiceResponse<dynamic>> AddImages(IFormFile image)
        {
            var fileName = image.Name + Path.GetExtension(image.FileName);

            // Define the path to save the uploaded image
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
            var imageDetails = new Image
            {
                ImageLocation = filePath,
                ImageName = fileName,
            };
            _context.Add(imageDetails);
            await _context.SaveChangesAsync();
            // Save the image to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            return new ServiceResponse<dynamic>()
            {
                Data = "Upload successfully!"
            };
        }
    }
}
