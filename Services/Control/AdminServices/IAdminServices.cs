using CityFlims.Models;
using CityFlims.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace CityFlims.Services.Control.AdminServices
{
    public interface IAdminServices
    {
        Task<ServiceResponse<dynamic>> GetImages();
        Task<ServiceResponse<dynamic>> UploadImages(ImageModel model);
    }
}
