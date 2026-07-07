using System;
using System.Collections.Generic;

namespace PetopiaWebApi.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? PhoneNo { get; set; }

    public string? Location { get; set; }

    public string UserRole { get; set; } = null!;

    public virtual ICollection<AdoptionStatus> AdoptionStatuses { get; set; } = new List<AdoptionStatus>();

    public virtual ICollection<PetProfile> PetProfiles { get; set; } = new List<PetProfile>();
}
