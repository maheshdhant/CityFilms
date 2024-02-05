namespace CityFilms.Models
{
    public class EmailModel
    {
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public IFormFile? Attachment { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Sender { get; set; }
    }

    public class EmailConfig
    {
        public string Email { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
