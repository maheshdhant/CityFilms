using CityFilms.Entity;
using CityFilms.Models;
using CityFilms.Models.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityFilms.Services.Api.Auth
{
    public class AuthorizationServices : IAuthorizationServices
    {

        private readonly string _secret;
        private readonly string _expDate;
        private IConfiguration _config;
        public AuthorizationServices(IConfiguration config) 
        {
            _config = config;
            _secret = config.GetSection("JwtConfig").GetSection("Secret").Value;
            _expDate = config.GetSection("JwtConfig").GetSection("ExpirationInMinutes").Value;
        }

        public async Task<ServiceResponse<string>> RegisterUser(NewUserRegisterModel model)
        {
            Random rnd = new Random();
            var userName = model.UserName;
            var email = model.Email;
            var password = model.Password;
            if (userName.Length < 4)
            {
                return new ServiceResponse<string>() { Data = "Username must be longer than 4 characters!" };
            }

            // check if email exists
            using var ent = new CityfilmsDataContext();
            var emailcheck = await ent.UserProfiles.AnyAsync(x => x.Email == model.Email);
            if (emailcheck == true)
            {
                return new ServiceResponse<string>() { Data = "Email already registered!" };
            }

            // check if username exists
            var checkUser = await ent.UserProfiles.FirstOrDefaultAsync(x => x.UserName.ToLower() == userName.ToLower());
            if (checkUser != null)
            {
                return new ServiceResponse<string>() { Data = "Username not available!" };
            }

            var pass = new UserPassword();
            var salt = pass.GenerateSalt(userName);
            var hashPass = pass.GetHmac(password, salt);
            var userId = Guid.NewGuid();

            var user = new UrUser()
            {
                Password = hashPass,
                PasswordSalt = salt,
                IsActive = true,
                IsLockedOut = false,
                CreatedDate = DateTime.UtcNow,
                FailedPasswordAttemptCount = 0,
                UserId = userId,
                UserTypeId = null,
            };
            ent.UrUsers.Add(user);

            var profile = new UserProfile()
            {
                UserId = userId,
                Email = email,
                UserName = userName,
                UserProfileId = Guid.NewGuid(),
            };
            ent.UserProfiles.Add(profile);

            await ent.SaveChangesAsync();


            return new ServiceResponse<string>()
            {
                Data = "Registration completed!",
            };
        }
        public async Task<ServiceResponse<dynamic>> Login(LoginModel model)
        {

            if (!IsUserLocked(model.UserName))
            {
                return new ServiceResponse<dynamic>()
                {
                    Data = false
                };
            }

            var userId = await ValidateUser(model.UserName, model.Password);
            if (userId == Guid.Empty)
            {
                return new ServiceResponse<dynamic>()
                {
                    Data = false
                };
            }
            return await UserDetail(userId);
        }
        private bool IsUserLocked(string userName)
        {
            using var ent = new CityfilmsDataContext();
            return ent.UserProfiles.Include(x => x.User).Any(x => x.User.IsActive && !x.User.IsLockedOut && x.UserName.Equals(userName));
        }
        private async Task<Guid> ValidateUser(string userName, string password)
        {
            using var ent = new CityfilmsDataContext();
            var failedPasswordAttempt = 10;
            try
            {
                //var loginLog = new UrUserLoginLog()
                //{
                //    CreatedDate = DateTime.UtcNow,
                //    CreatedTerminalId = _webHelper.GetCurrentIpAddress(),
                //    Details = _webHelper.GetCurrentIpAddress(),
                //    IsDeleted = false
                //};
                var pass = new UserPassword();
                var user = ent.UserProfiles.Include(x => x.User).FirstOrDefault(x => x.UserName.ToLower() == userName.Trim().ToLower() && !x.IsDeleted && x.User.IsActive);
                if (user == null) return Guid.Empty;
                var hassPass = pass.GetHmac(password, user.User.PasswordSalt);
                //loginLog.UserId = user.UserId;
                if (hassPass == user.User.Password)
                {
                    user.User.LastActivityDate = DateTime.UtcNow;
                    user.User.LastLoginDate = DateTime.UtcNow;
                    user.User.FailedPasswordAttemptCount = 0;
                    //loginLog.IsSuccess = true;
                    await ent.SaveChangesAsync();
                    return user.UserId;
                }
                else
                {
                    //loginLog.IsSuccess = false;
                    user.User.FailedPasswordAttemptCount = user.User.FailedPasswordAttemptCount + 1;
                    if (user.User.FailedPasswordAttemptCount == failedPasswordAttempt)
                    {
                        user.User.IsLockedOut = true;
                        user.User.LastLockoutDate = DateTime.UtcNow;
                    }
                    user.User.FailedPasswordAttemptWindowStart = DateTime.UtcNow;
                    user.User.LastActivityDate = DateTime.UtcNow;
                    await ent.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var xx = ex.Message;
            }
            return Guid.Empty;
        }
        private async Task<ServiceResponse<dynamic>> UserDetail(Guid userId)
        {
            using var ent = new CityfilmsDataContext();
            var userProfile = new UserProfile();
            userProfile = await ent.UserProfiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userId);
            if (userProfile == null) { return new ServiceResponse<dynamic>() { Data = null }; }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            //var userRoles = await ent.UrUserRoles.Where(x => x.UserId == userProfile.UserId).Select(x => x.UserRoleType.RoleTypeName).ToListAsync();


            string userTypeId = userProfile.User.UserTypeId == null ? "0" : userProfile.User.UserTypeId.ToString();
            int UserInfoTypeId = int.Parse(userTypeId);
            var userClaims = new List<Claim>();
            //foreach (var item in userRoles)
            //{
            //    userClaims.Add(new Claim(ClaimTypes.Role, item));
            //}
            //string FullName = userProfile.FullNameNp;

            //userClaims.Add(new Claim("FirstName", FullName));
            userClaims.Add(new Claim("UserName", userProfile.UserName));
            userClaims.Add(new Claim("UserTypeId", userTypeId));



            //var permissionList = await GetPermissionList(userId, UserInfoTypeId);
            //if (permissionList.Any())
            //{
            //    SetClaims.CreateClaims(permissionList, userClaims);
            //}


            var claimsIdentity = new ClaimsIdentity(userClaims);

            var tokenExpiry = DateTime.UtcNow.AddMinutes(double.Parse(_expDate));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiry,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenObj = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(tokenObj);

            var loginDetail = new
            {
                //userProfile.FirstName,
                userProfile.UserName,
                jwtToken,
                UserTypeId = userProfile.User.UserTypeId == null ? 0 : userProfile.User.UserTypeId,

                tokenExpiry = tokenExpiry.ToString("ddd, dd MMM yyyy hh:mm:ss") + " UTC"
            };

            _webHelper.SetAuthorization(jwtToken);
            return new ServiceResponse<dynamic>() { Data = loginDetail };
        }
    }
}
