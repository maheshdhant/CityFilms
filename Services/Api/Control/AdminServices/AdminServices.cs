using CityFilms.Entity;
using CityFilms.Models;
using CityFilms.Models.Response;
using CityFilms.Services.Helper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Diagnostics;

namespace CityFilms.Services.Api.Control.AdminServices
{
    public class AdminServices : BaseService, IAdminServices
    {
        private readonly CityfilmsDataContext _context;
        public AdminServices(CityfilmsDataContext context, IWebHelper webHelper)
        {
            _webHelper = webHelper;
            _context = context;
        }
        public async Task<ServiceResponse<dynamic>> GetImages()
        {
            await GetCurrentUser();

            var logoLocation = await _context.Images.Where(x => x.ImageTypeId == 2 || x.ImageTypeId == 1).OrderBy(x => x.DateUpdated).Select(x => new ImageModel()
            {
                ImageLocation = x.ImageLocation,
            }).ToListAsync();

            return new ServiceResponse<dynamic>()
            {
                Data = logoLocation
            };
        }
        public async Task<ServiceResponse<dynamic>> GetBackgroundImages()
        {
            await GetCurrentUser();
            var backgroundImages = await _context.Images.Where(x => x.ImageTypeId == 2).OrderByDescending(x => x.DateUpdated).Select(x => new ImageModel()
            {
                ImageLocation = x.ImageLocation,
                ImageId = x.ImageId,
                IsSelected = x.IsSelected,
            }).ToListAsync();

            return new ServiceResponse<dynamic>()
            {
                Data = backgroundImages
            };
        }
        public async Task<ServiceResponse<dynamic>> UploadImages(ImageModel model)
        {
            await GetCurrentUser();
            var fileName = model.ImageFile.FileName;
            var filePath = "";

            if (model.ImageFile != null)
            {
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
                    return new ServiceResponse<dynamic>()
                    {
                        Data = true,
                    };
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
                    filePath = Path.Combine("wwwroot", "uploads", "images", "background", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    // save image details to database
                    filePath = Path.Combine(Path.Combine("..", "uploads", "images", "background"), fileName);
                    var imageDetails = new Image
                    {
                        ImageLocation = filePath,
                        ImageName = fileName,
                        ImageTypeId = model.ImageTypeId,
                        DateUpdated = DateTime.Now,
                        IsSelected = false,
                    };
                    _context.Images.Add(imageDetails);
                    await _context.SaveChangesAsync();

                    return new ServiceResponse<dynamic>()
                    {
                        Data = true
                    };
                }
            }
            return new ServiceResponse<dynamic>()
            {
                Data = false
            };
        }
        public async Task<ServiceResponse<dynamic>> DeleteBackgroundImage(int imageId)
        {
            await GetCurrentUser();
            var imgToRemove = await _context.Images.Where(x => x.ImageId == imageId).FirstOrDefaultAsync();
            if (imgToRemove != null)
            {
                if (imgToRemove.IsSelected != true)
                {
                    if (imgToRemove.ImageName != null)
                    {
                        var filePath = Path.Combine("wwwroot", "uploads", "images", "background", imgToRemove.ImageName); ;
                        File.Delete(filePath);
                    }
                    _context.Images.RemoveRange(imgToRemove);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return new ServiceResponse<dynamic>()
                    {
                        Data = false,
                    };
                }
            }

            return new ServiceResponse<dynamic>()
            {
                Data = true,
            };
        }
        public async Task<ServiceResponse<dynamic>> SelectBackgroundImage(int Id)
        {
            await GetCurrentUser();
            using var ent = new CityfilmsDataContext();

            // deselect previous background
            var obj = await ent.Images.Where(x => x.IsSelected == true && x.ImageTypeId == 2 && x.ImageId != Id).FirstOrDefaultAsync();
            if (obj != null)
            {
                obj.IsSelected = false;
                await ent.SaveChangesAsync();
            }

            // select background
            obj = await ent.Images.Where(x => x.ImageId == Id).FirstOrDefaultAsync();
            if (obj != null)
            {
                if (obj.IsSelected != true)
                {
                    obj.IsSelected = true;
                    await ent.SaveChangesAsync();
                }
                else
                {
                    return new ServiceResponse<dynamic>()
                    {
                        Data = false
                    };
                }
            }


            return new ServiceResponse<dynamic>()
            {
                Data = true,
            };
        }
        public async Task<ServiceResponse<dynamic>> AddPartnerInfo(PartnerModel model)
        {
            try
            {
                await GetCurrentUser();
            }catch (Exception ex)
            {
                return new ServiceResponse<dynamic>() { Data = false };
            }
            var fileName = model.PartnerImage.FileName;
            var filePath = "";
            filePath = Path.Combine("wwwroot", "uploads", "images", "partners");

            // creates directory if does not exists
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = Path.Combine("wwwroot", "uploads", "images", "partners", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.PartnerImage.CopyToAsync(stream);
            }

            // save image details to database
            filePath = Path.Combine(Path.Combine("..", "uploads", "images", "partners"), fileName);
            var partnerImageDetails = new Image
            {
                ImageLocation = filePath,
                ImageName = fileName,
                ImageTypeId = 3,
                DateUpdated = DateTime.Now,
                IsSelected = false,
            };
            _context.Images.Add(partnerImageDetails);
            await _context.SaveChangesAsync();

            var partnerDetail = new Partner
            {
                PartnerImageId = partnerImageDetails.ImageId,
                PartnerName = model.PartnerName,
                PartnerEmail = model.PartnerEmail,
                PartnerPhone = model.PartnerPhone,
                ParnterDescription = model.PartnerDescription,
                PartnerWebsite = model.PartnerWebsite,
            };
            _context.Partners.Add(partnerDetail);
            await _context.SaveChangesAsync();

            return new ServiceResponse<dynamic>()
            {
                Data = true,
            };
        }
        public async Task<ServiceResponse<dynamic>> GetPartnerInfo()
        {
            await GetCurrentUser();
            using var ent = new CityfilmsDataContext();
            var model = await ent.Partners.Include(x => x.PartnerImage).Select(x => new PartnerModel
            {
                PartnerId = x.PartnerId,
                PartnerName = x.PartnerName,
                PartnerDescription = x.ParnterDescription,
                PartnerWebsite = x.PartnerWebsite,
                PartnerPhone = x.PartnerPhone,
                PartnerEmail = x.PartnerEmail,
                PartnerImageId = x.PartnerImageId,
                PartnerImageLocation = x.PartnerImage.ImageLocation,
            }).ToListAsync();

            return new ServiceResponse<dynamic>()
            {
                Data = model,
            };
        }
        public async Task<ServiceResponse<dynamic>> EditPartnerInfo(PartnerModel model)
        {
            try
            {
                await GetCurrentUser();
            }
            catch (Exception ex)
            {
                return new ServiceResponse<dynamic>() { Data = false };
            }

            using var ent = new CityfilmsDataContext();

            var partnerId = model.PartnerId;
            var filePath = "";
            int? partnerImageId = 0;

            var partner = await ent.Partners.Where(x => x.PartnerId == model.PartnerId).FirstOrDefaultAsync();
            if (partner != null)
            {
                partner.PartnerName = model.PartnerName;
                partner.PartnerEmail = model.PartnerEmail;
                partner.PartnerPhone = model.PartnerPhone;
                partner.ParnterDescription = model.PartnerDescription;
                partner.PartnerWebsite = model.PartnerWebsite;
                await ent.SaveChangesAsync();
            };
            partnerImageId = await ent.Partners.Where(x => x.PartnerId == model.PartnerId).Select(x => x.PartnerImageId).FirstOrDefaultAsync();
            
            if (model.PartnerImage != null)
            {
                var fileName = model.PartnerImage.FileName;
                filePath = Path.Combine("wwwroot", "uploads", "images", "partners");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath = Path.Combine("wwwroot", "uploads", "images", "partners", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.PartnerImage.CopyToAsync(stream);
                }

                var obj = await ent.Images.Where(x => x.ImageId == partnerImageId).FirstOrDefaultAsync();
                if (obj != null)
                {
                    obj.DateUpdated = DateTime.Now;
                    obj.ImageLocation = Path.Combine("..", "uploads", "images", "partners", fileName);
                    obj.ImageName = fileName;
                    await ent.SaveChangesAsync();
                }
            }

            return new ServiceResponse<dynamic>()
            {
                Data = true,
            };
        }

        public async Task<ServiceResponse<dynamic>> GetCompanyProfile()
        {
            using var ent = new CityfilmsDataContext();
            var profile = await ent.CompanyProfiles.FirstOrDefaultAsync();
            if (profile != null)
            {
                return new ServiceResponse<dynamic>()
                {
                    Data = profile,
                };
            }
            return new ServiceResponse<dynamic>()
            {
                Data = false,
            };
        }
        public async Task<ServiceResponse<dynamic>> EditCompanyProfile(CompanyProfileModel model)
        {
            await GetCurrentUser();
            using var ent = new CityfilmsDataContext();

            var profile = await ent.CompanyProfiles.FirstOrDefaultAsync();
            if (profile == null)
            {
                CompanyProfile newProfile = new CompanyProfile();
                newProfile.CompanyName = model.CompanyName;
                newProfile.CompanyPhone = model.CompanyPhone;
                newProfile.CompanyMail = model.CompanyMail;
                newProfile.CompanyAddress = model.CompanyAddress;
                newProfile.CompanyTagline = model.CompanyTagline;
                ent.CompanyProfiles.Add(newProfile);
                await ent.SaveChangesAsync();
            }
            else
            {
                profile.CompanyName = model.CompanyName;
                profile.CompanyPhone = model.CompanyPhone;
                profile.CompanyMail = model.CompanyMail;
                profile.CompanyAddress = model.CompanyAddress;
                profile.CompanyTagline = model.CompanyTagline;
                await ent.SaveChangesAsync();
            }
            return new ServiceResponse<dynamic>()
            {
                Data = true,
            };
        }

    }
}
