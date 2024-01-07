using Microsoft.AspNetCore.Http;

namespace CityFilms.Models
{
    public class ImageModel
    {
        public string? ImageName { get; set; }
        public string? ImageLocation { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int? ImageTypeId { get; set; }
    }
}
