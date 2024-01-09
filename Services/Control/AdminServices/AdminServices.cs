using CityFilms.Entity;
using CityFilms.Models;
using CityFilms.Models.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityFilms.Services.Control.AdminServices
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

            var logoLocation = await _context.Images.Where(x => x.ImageTypeId == 2).OrderBy(x => x.DateUpdated).Select(x => new ImageModel()
            {
                ImageLocation = x.ImageLocation,
            }).ToListAsync();

            return new ServiceResponse<dynamic>()
            {
                Data = logoLocation
            };
        }

        public async Task<ServiceResponse<dynamic>> UploadImages(ImageModel model)
        {
            var fileName = model.ImageFile.FileName;
            var filePath = "";

            //for logo
            if (model.ImageTypeId == 1)
            {
                fileName = "logo.jpg";
                // Define the path to save the uploaded image
                filePath = Path.Combine("wwwroot", "uploads", "images", "logo", fileName);
                var imageDetails = new Image
                {
                    ImageLocation = Path.Combine("..", "uploads", "images", "logo", fileName),
                    ImageName = fileName,
                    ImageTypeId = model.ImageTypeId,
                    DateUpdated = DateTime.Now,
                };
                var logoInDb = await _context.Images.Where(x => x.ImageTypeId == 1).Select(x => new ImageModel()
                {
                    ImageLocation = x.ImageLocation,
                }).ToListAsync();
                if (logoInDb.Count() == 0)
                {
                    _context.Images.Add(imageDetails);
                    await _context.SaveChangesAsync();
                }
                
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                // Save the image to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
            }
            // for background cover
            if (model.ImageTypeId == 2)
            {
                filePath = Path.Combine("wwwroot", "uploads", "images", "background");
                // creates directory if does not exists
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                // Save the image to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                filePath = Path.Combine(Path.Combine("..", "uploads", "images", "background"), fileName);
                // save image details to database
                var imageDetails = new Image
                {
                    ImageLocation = filePath,
                    ImageName = fileName,
                    ImageTypeId = model.ImageTypeId,
                    DateUpdated = DateTime.Now,
                };
                _context.Images.Add(imageDetails);
                await _context.SaveChangesAsync();

            }

            return new ServiceResponse<dynamic>()
            {
                Data = "Upload successfully!"
            };
        }
    }
}
