using Microsoft.AspNetCore.Http;

namespace CityFilms.Models
{
    public class ImageModel
    {
        public int ImageId { get; set; }
        public string? ImageName { get; set; }
        public string? ImageLocation { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int? ImageTypeId { get; set; }
        public bool? IsSelected { get; set; }
    }
}
