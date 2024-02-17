using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace CityFilms.Services.Helper
{
    public class WebHelper : IWebHelper
    {
        public enum CookiesNameValue
        {
            Token = 0
        }
        protected virtual bool IsRequestAvailable()
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
                return false;

            try
            {
                if (_httpContextAccessor.HttpContext.Request == null)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
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
            _httpContextAccessor.HttpContext.Request.Headers.TryGetValue(HeaderNames.Authorization, out StringValues authToken);
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
        public void ClearCookie()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("Token");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("pgwToken");
        }
    }
}
