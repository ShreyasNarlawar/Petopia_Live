using System;
using System.Collections.Generic;

namespace PetopiaWebApi.Models;

public partial class Breed
{
    public int BreedId { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual PetCategory Category { get; set; } = null!;

    public virtual ICollection<PetProfile> PetProfiles { get; set; } = new List<PetProfile>();
}
