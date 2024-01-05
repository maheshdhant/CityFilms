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

            var logoLocation = await _context.Images.Where(x => x.ImageTypeId == 1).Select(x => new ImageModel()
            {
                ImageLocation = x.ImageLocation,
            }).FirstOrDefaultAsync();

            return new ServiceResponse<dynamic>()
            {
                Data = logoLocation
            };
        }

        public async Task<ServiceResponse<dynamic>> UploadImages(ImageModel model)
        {
            var fileName = model.ImageFile.FileName;
            var filePath = "";

            if (model.ImageTypeId == 1)
            {
                fileName = "logo.jpg";
                // Define the path to save the uploaded image
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "images", "logo", fileName);
                var imageDetails = new Image
                {
                    ImageLocation = filePath,
                    ImageName = fileName,
                    ImageTypeId = model.ImageTypeId,
                };
                _context.Add(imageDetails);
                await _context.SaveChangesAsync();
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            // Save the image to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(stream);
            }
            return new ServiceResponse<dynamic>()
            {
                Data = "Upload successfully!"
            };
        }
    }
}
