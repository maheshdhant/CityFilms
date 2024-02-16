namespace CityFilms.Services.Helper
{
    public partial interface IWebHelper
    {
        void SetAuthorization(string Authorization);
        string GetUserNameFromJwt();
        void ClearCookie();
    }
}
