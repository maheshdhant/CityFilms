using Microsoft.Extensions.Primitives;

namespace CityFilms.Services.Helper
{
    public class WebHelper : IWebHelper
    {
        public enum CookiesNameValue
        {
            Token = 0
        }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WebHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void SetAuthorization(string Authorization)
        {
            try
            {
                var option = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1),
                    Secure = true,
                    IsEssential = true,
                    HttpOnly = false
                };
                Authorization = "Bearer " + Authorization;
                _httpContextAccessor.HttpContext.Response.Cookies.Append(CookiesNameValue.Token.ToString(), Authorization, option);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
        public string GetUserNameFromJwt()
        {
            _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authToken);
            var strUserName = string.Empty;
            if (string.IsNullOrEmpty(authToken)) return strUserName;
            var strToken = authToken.ToString().Substring(7);
            var tokenObj = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(strToken);
            var userName = tokenObj.Claims.ToList().FirstOrDefault(x => x.Type == "UserName");
            if (userName != null)
            {
                strUserName = userName.Value;
            }
            return strUserName;
        }
    }
}
