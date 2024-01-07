using System;
using System.Collections.Generic;

namespace CityFilms.Entity;

public partial class ImageType
{
    public int ImageTypeId { get; set; }

    public string? ImageTypeName { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
