using System;
using System.Collections.Generic;

namespace CityFilms.Entity;

public partial class Image
{
    public int ImageId { get; set; }

    public string? ImageName { get; set; }

    public string? ImageLocation { get; set; }

    public int? ImageTypeId { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual ImageType? ImageType { get; set; }
}
