using System;
using System.Collections.Generic;

namespace CityFilms.Entity;

public partial class MailLog
{
    public int MailLogId { get; set; }

    public string? Subject { get; set; }

    public string? SentBy { get; set; }

    public string? SentTo { get; set; }

    public DateTime? CreatedDate { get; set; }
}
