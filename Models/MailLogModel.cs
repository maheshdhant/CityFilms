namespace CityFilms.Models
{
    public class MailLogModel
    {
        public string? Subject { get; set; }

        public string? SentBy { get; set; }

        public string? SentTo { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
