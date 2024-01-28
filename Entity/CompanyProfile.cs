using System;
using System.Collections.Generic;

namespace CityFilms.Entity;

public partial class CompanyProfile
{
    public int CompanyProfileId { get; set; }

    public string? CompanyName { get; set; }

    public string? CompanyAddress { get; set; }

    public string? CompanyPhone { get; set; }

    public string? CompanyMail { get; set; }

    public string? CompanyTagline { get; set; }
}
