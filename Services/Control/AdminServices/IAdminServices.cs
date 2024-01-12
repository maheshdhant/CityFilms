using CityFilms.Models;
using CityFilms.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace CityFilms.Services.Control.AdminServices
{
    public interface IAdminServices
    {
        Task<ServiceResponse<dynamic>> GetImages();
        Task<ServiceResponse<dynamic>> GetBackgroundImages();
        Task<ServiceResponse<dynamic>> UploadImages(ImageModel model);
    }
}
