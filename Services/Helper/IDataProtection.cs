namespace CityFilms.Services.Helper
{
    public interface IDataProtection
    {
        string Decode(string data);
        string Encode(string data);
    }
}
