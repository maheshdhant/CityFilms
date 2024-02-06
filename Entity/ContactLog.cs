using System;
using System.Collections.Generic;

namespace CityFilms.Entity;

public partial class ContactLog
{
    public int ContactLogId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Message { get; set; }

    public DateTime? CreatedDate { get; set; }
}
