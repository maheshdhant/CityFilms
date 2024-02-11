using System;
using System.Collections.Generic;

namespace CityFilms.Entity;

public partial class UserType
{
    public int UserTypeId { get; set; }

    public string UserType1 { get; set; } = null!;

    public virtual ICollection<UrUser> UrUsers { get; set; } = new List<UrUser>();
}
