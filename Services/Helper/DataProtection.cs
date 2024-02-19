using Microsoft.AspNetCore.DataProtection;

namespace CityFilms.Services.Helper
{
    public class DataProtection : IDataProtection
    {
        private readonly IDataProtector protector;
        public readonly string UniqueCode = "cUhZuo792Ktvl";
        public DataProtection(IDataProtectionProvider dataProtectionProvider)
        {
            protector = dataProtectionProvider.CreateProtector(UniqueCode);
        }
        public string Decode(string data)
        {
            try
            {
                return protector.Unprotect(data);
            }
            catch (Exception)
            {

                return "";
            }


        }
        public string Encode(string data)
        {
            try
            {
                return protector.Protect(data);
            }
            catch (Exception)
            {

                return "";
            }

        }
    }
}
