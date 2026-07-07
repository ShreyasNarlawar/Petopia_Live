using System;
using System.Collections.Generic;

namespace PetopiaWebApi.Models;

public partial class PetCategory
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Breed> Breeds { get; set; } = new List<Breed>();

    public virtual ICollection<PetProfile> PetProfiles { get; set; } = new List<PetProfile>();
}
