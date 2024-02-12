using System;
using System.Collections.Generic;

namespace CityFilms.Entity;

public partial class UserProfile
{
    public Guid UserProfileId { get; set; }

    public Guid UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string? Email { get; set; }

    public bool IsDeleted { get; set; }

    public virtual UrUser User { get; set; } = null!;
}
