using CityFilms.Entity;
using CityFilms.Models;
using CityFilms.Models.Response;
using Microsoft.EntityFrameworkCore;

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
    }
}
