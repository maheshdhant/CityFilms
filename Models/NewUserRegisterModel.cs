using System.ComponentModel.DataAnnotations;

namespace CityFilms.Models
{
    public class NewUserRegisterModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public class CurrentUserModel
    {
        public CurrentUserModel()
        {

        }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserLogName { get; set; }
        public DateTime CurrentDateTime => DateTime.Now;

        public string Token { get; set; }
        public string DeviceId { get; set; }
        public string TimeZoneId { get; set; }
        public long? UserTypeId { get; set; }
        public int? EmployeeTypeId { get; set; }
    }
}
