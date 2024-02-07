namespace CityFilms.Models
{
    public class ContactLogModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; } = "Booking";
        public string? Message { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
