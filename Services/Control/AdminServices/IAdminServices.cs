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
        Task<ServiceResponse<dynamic>> DeleteBackgroundImage(int imageId);
        Task<ServiceResponse<dynamic>> SelectBackgroundImage(int Id);
        Task<ServiceResponse<dynamic>> AddPartnerInfo(PartnerModel model);
        Task<ServiceResponse<dynamic>> GetPartnerInfo();
        Task<ServiceResponse<dynamic>> EditPartnerInfo(PartnerModel model);
        Task<ServiceResponse<dynamic>> GetCompanyProfile();
        Task<ServiceResponse<dynamic>> EditCompanyProfile(CompanyProfileModel model);
    }
}
