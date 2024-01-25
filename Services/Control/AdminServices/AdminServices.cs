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
                        Data = "Upload successfull!"
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
            }
            return new ServiceResponse<dynamic>()
            {
                Data = "Upload failed!"
            };
        }
        public async Task<ServiceResponse<dynamic>> DeleteBackgroundImage(int imageId)
        {
            var imgToRemove = await _context.Images.Where(x => x.ImageId == imageId).FirstOrDefaultAsync();
            if (imgToRemove != null)
            {
                if(imgToRemove.IsSelected != true)
                {
                    if (imgToRemove.ImageName != null)
                    {
                        var filePath = Path.Combine("wwwroot", "uploads", "images", "background", imgToRemove.ImageName); ;
                        System.IO.File.Delete(filePath);
                    }
                    _context.Images.RemoveRange(imgToRemove);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return new ServiceResponse<dynamic>()
                    {
                        Data = "Current background image cannot be deleted!"
                    };
                }
            }
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
        public async Task<ServiceResponse<dynamic>> SelectBackgroundImage(int Id)
        {
            using var ent = new CityfilmsDataContext();
            Image obj = new Image();
            
            // deselect previous background
            obj = await ent.Images.Where(x => x.IsSelected == true && x.ImageTypeId == 2).FirstOrDefaultAsync();
            if (obj != null)
            {
                obj.IsSelected = false;
                await ent.SaveChangesAsync();
            }

            // select background
            obj= await ent.Images.Where(x => x.ImageId == Id).FirstOrDefaultAsync();
            if (obj != null)
            {
                if(obj.IsSelected != true)
                {
                    obj.IsSelected = true;
                    await ent.SaveChangesAsync();
                }
                else
                {
                    return new ServiceResponse<dynamic>()
                    {
                        Data = "Already selected!"
                    };
                }
            }
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
        public async Task<ServiceResponse<dynamic>> AddPartnerInfo(PartnerModel model)
        {
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
                Data = "Partner's Info Uploaded Successfully!",
            };
        }
        public async Task<ServiceResponse<dynamic>> GetPartnerInfo()
        {
            using var ent = new CityfilmsDataContext();
            List<PartnerModel> model = new List<PartnerModel>();
            var partnerInfo = await ent.Partners.Include(x => x.PartnerImage).Select(x => new PartnerModel
            {
                PartnerId = x.PartnerId,
                PartnerName = x.PartnerName,
                PartnerDescription = x.ParnterDescription,
                PartnerWebsite = x.PartnerWebsite,
                PartnerPhone= x.PartnerPhone,
                PartnerEmail = x.PartnerEmail,
                PartnerImageId = x.PartnerImageId,
                PartnerImageLocation = x.PartnerImage.ImageLocation,
            }).ToListAsync();
            model = partnerInfo;
            return new ServiceResponse<dynamic>()
            {
                Data = model,
            };
        }
        public async Task<ServiceResponse<dynamic>> EditPartnerInfo(PartnerModel model)
        {
            using var ent = new CityfilmsDataContext();

            var partnerId = model.PartnerId;
            var fileName = model.PartnerImage.FileName;
            var filePath = "";

            Image obj = new Image();

            if (model.PartnerImage != null)
            {
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

                obj = await ent.Images.Where(x => x.ImageId == model.PartnerImageId).FirstOrDefaultAsync();
                if (obj != null)
                {
                    obj.DateUpdated = DateTime.Now;
                    obj.ImageLocation = Path.Combine("wwwroot", "uploads", "images", "partners", fileName);
                    obj.ImageName = fileName;
                    await ent.SaveChangesAsync();
                }
            }

            Partner partner = new Partner();
            partner = await ent.Partners.Where(x => x.PartnerId == model.PartnerId).FirstOrDefaultAsync();
            if(partner != null)
            {
                partner.PartnerName = model.PartnerName;
                partner.PartnerEmail = model.PartnerEmail;
                partner.PartnerPhone = model.PartnerPhone;
                partner.ParnterDescription = model.PartnerDescription;
                partner.PartnerWebsite = model.PartnerWebsite;
            await _context.SaveChangesAsync();
            };

            List<PartnerModel> partnerInfo = new List<PartnerModel>();
            partnerInfo = await ent.Partners.Include(x => x.PartnerImage).Select(x => new PartnerModel
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
                Data = partnerInfo,
            };
        }
    }
}
