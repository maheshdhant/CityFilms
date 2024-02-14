using CityFilms.Entity;
using CityFilms.Models;
using CityFilms.Services.Helper;
using Microsoft.EntityFrameworkCore;

namespace CityFilms.Services
{
    public class BaseService: IBaseService
    {
        public IWebHelper _webHelper;
        public CurrentUserModel User { get; set; }
        public BaseService()
        {
            User = new CurrentUserModel();
        }
        public async Task GetCurrentUser()
        {
            using var ent = new CityfilmsDataContext();
            var userProfile = await ent.UserProfiles.Where(x => x.UserName == _webHelper.GetUserNameFromJwt())
                .Select(x => new { x.UserName, x.UserId }).FirstOrDefaultAsync();

            var UserTypeId = await ent.UrUsers.Where(x => x.UserId == userProfile.UserId).Select(x => x.UserTypeId).FirstOrDefaultAsync();

            if (userProfile == null) return;
            User.UserLogName = userProfile.UserName;
            User.UserName = userProfile.UserName;
            User.UserId = userProfile.UserId;
            User.UserTypeId = UserTypeId;
        }
    }
}
