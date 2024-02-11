using System;
using System.Collections.Generic;

namespace CityFilms.Entity;

public partial class UrUser
{
    public Guid UserId { get; set; }

    public string? Password { get; set; }

    public string? PasswordSalt { get; set; }

    public bool? IsLockedOut { get; set; }

    public bool? IsActive { get; set; }

    public int? FailedPasswordAttemptCount { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UserTypeId { get; set; }

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();

    public virtual UserType? UserType { get; set; }
}
