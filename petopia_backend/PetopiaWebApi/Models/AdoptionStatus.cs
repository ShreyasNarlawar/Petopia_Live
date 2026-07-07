using System;
using System.Collections.Generic;

namespace PetopiaWebApi.Models;

public partial class AdoptionStatus
{
    public int StatusId { get; set; }

    public int PetId { get; set; }

    public int? UserId { get; set; }

    public string Status { get; set; } = null!;

    public virtual PetProfile Pet { get; set; } = null!;

    public virtual User? User { get; set; }
}
