using System;
using System.Collections.Generic;

namespace CityFlims.Entity;

public partial class Image
{
    public int ImageId { get; set; }

    public string? ImageName { get; set; }

    public string? ImageLocation { get; set; }

    public int? ImageTypeId { get; set; }

    public virtual ImageType? ImageType { get; set; }
}
