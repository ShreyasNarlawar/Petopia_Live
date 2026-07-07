using System;
using System.Collections.Generic;

namespace PetopiaWebApi.Models;

public partial class PetImage
{
    public int ImageId { get; set; }

    public int PetId { get; set; }

    public string ImagePath { get; set; } = null!;

    public virtual PetProfile Pet { get; set; } = null!;
}
