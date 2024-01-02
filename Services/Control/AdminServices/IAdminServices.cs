using CityFlims.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityFlims.Services.Control.AdminServices
{
    public interface IAdminServices
    {
        Task<IEnumerable<ImageModel>> GetImages();
        Task AddImages(ImageModel image);
    }
}
