namespace CityFilms.Models
{
    public class PartnerModel
    {
        public int PartnerId { get; set; }
        public string? PartnerName { get; set;}
        public string? PartnerDescription { get; set;}
        public string? PartnerPhone { get; set;}
        public string? PartnerEmail { get; set;}
        public string? PartnerWebsite { get; set;}
        public int? PartnerImageId { get; set; }
        public int? PartnerImageLocation{ get; set; }
        public IFormFile? PartnerImage { get; set; }
    }
}
