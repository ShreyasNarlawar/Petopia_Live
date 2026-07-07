using System;
using System.Collections.Generic;

namespace PetopiaWebApi.Models;

public partial class PetProfile
{
    public int PetId { get; set; }

    public string? Name { get; set; }

    public int CategoryId { get; set; }

    public int BreedId { get; set; }

    public int? Age { get; set; }

    public string? Size { get; set; }

    public string? Gender { get; set; }

    public decimal? Weight { get; set; }

    public bool? Vaccinated { get; set; }

    public bool? Spayed { get; set; }

    public string? Health { get; set; }

    public string? Needs { get; set; }

    public bool? HouseTrained { get; set; }

    public bool? GoodWithKids { get; set; }

    public bool? GoodWithPets { get; set; }

    public string? Personality { get; set; }

    public decimal? MonthlyExpenses { get; set; }

    public string? IsRegisteredWithGovt { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<AdoptionStatus> AdoptionStatuses { get; set; } = new List<AdoptionStatus>();

    public virtual Breed Breed { get; set; } = null!;

    public virtual PetCategory Category { get; set; } = null!;

    public virtual ICollection<PetImage> PetImages { get; set; } = new List<PetImage>();

    public virtual User User { get; set; } = null!;
}
