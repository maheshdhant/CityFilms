using CityFlims.Models;

namespace CityFlims.Services.Control.AdminServices
{
    public interface IAdminServices
    {
        Task<IEnumerable<ImageModel>> GetImages();
        void AddImage(ImageModel image);
    }
}
