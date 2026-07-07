using System;
using System.Collections.Generic;

namespace PetopiaWebApi.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;
}
