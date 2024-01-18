using System;
using System.Collections.Generic;

namespace CityFilms.Entity;

public partial class Partner
{
    public int PartnerId { get; set; }

    public string? PartnerName { get; set; }

    public string? ParnterDescription { get; set; }

    public string? PartnerPhone { get; set; }

    public string? PartnerEmail { get; set; }

    public string? PartnerWebsite { get; set; }

    public int? PartnerImageId { get; set; }

    public virtual Image? PartnerImage { get; set; }
}
