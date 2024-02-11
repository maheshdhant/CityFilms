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
}
