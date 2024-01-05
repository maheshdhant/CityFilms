using Microsoft.AspNetCore.Http;

namespace CityFlims.Models
{
    public class ImageModel
    {
        public string? ImageName { get; set; }
        public string? ImageLocation { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
