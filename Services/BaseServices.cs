using CityFilms.Entity;
using CityFilms.Services.Helper;

namespace CityFilms.Services
{
    public class BaseServices
    {
        public IWebHelper _webHelper;
        public async Task GetCurrentUser()
        {

            using var ent = new CityfilmsDataContext();
            var userProfile = await ent.UserProfiles.Where(x => x.UserName == _webHelper.GetUserNameFromJwt())
                .Select(x => new { x.UserName, x.FirstName, x.MiddleName, x.LastName, x.UserId, x.TimeZoneId }).FirstOrDefaultAsync();

            var UserTypeId = await ent.UrUsers.Where(x => x.UserId == userProfile.UserId).Select(x => x.UserTypeId).FirstOrDefaultAsync();
            var UserRoleId = await ent.UrUserRoleTypes.Where(x => x.RoleTypeName == _webHelper.GetRoleIdFromJwt()).Select(x => x.UserRoleTypeId).FirstOrDefaultAsync();
            var organizationId = ent.Organizations.Where(x => x.UserId == userProfile.UserId).Select(x => x.OrganizationId).FirstOrDefault();

            var companyid = ent.Companies.Where(x => x.UserId == userProfile.UserId).Select(x => x.CompanyId).FirstOrDefault();
            var UserEmployeeTypeId = await ent.UrUserProfiles.Where(x => x.UserId == userProfile.UserId).Select(x => x.EmployeeTypeId).FirstOrDefaultAsync();


            if (userProfile == null) return;
            User.UserLogName = userProfile.UserName + ":" + userProfile.FirstName + " " + userProfile.MiddleName + " " + userProfile.LastName;
            User.UserName = userProfile.UserName;
            User.UserId = userProfile.UserId;
            User.TimeZoneId = userProfile.TimeZoneId;
            User.PageSize = 20;
            User.RoleId = UserRoleId;
            User.OrganizationId = organizationId;
            User.CompanyId = companyid;
            User.UserTypeId = UserTypeId;
            User.EmployeeTypeId = UserEmployeeTypeId == null ? 0 : UserEmployeeTypeId;

            //setting.DefaultPageSize == null ? 5 : setting.DefaultPageSize.Value;
        }
    }
}
