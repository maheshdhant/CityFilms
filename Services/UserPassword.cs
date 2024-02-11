using System.Security.Cryptography;
using System.Text;

namespace CityFilms.Services
{

    public class UserPassword
    {
        public string GetHmac(string signatureString, string? secretKey = null)
        {
            if (string.IsNullOrEmpty(secretKey)) { secretKey = SecretKey; }
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secretKey);
            HMACSHA256 hmac = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(signatureString);
            byte[] hashmessage = hmac.ComputeHash(messageBytes);
            return ByteArrayToHexString(hashmessage);
        }
        public static string SecretKey => "9AD3B73E452E8FEE487A8F6124D3F125A5121629";
        private string ByteArrayToHexString(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder();
            string HexAlphabet = "0123456789ABCDEF"; foreach (byte B in Bytes) { Result.Append(HexAlphabet[B >> 4]); Result.Append(HexAlphabet[B & 0xF]); }
            return Result.ToString();
        }

        public string GenerateSalt(string userName)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(userName);
            HMACSHA1 hmac = new HMACSHA1(keyByte);
            byte[] messageBytes = encoding.GetBytes(DateTime.UtcNow.ToBinary().ToString());
            byte[] hashmessage = hmac.ComputeHash(messageBytes);
            return ByteArrayToHexString(hashmessage);
        }
    }
}
