using System;
using System.Collections.Generic;

namespace api;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Number { get; set; }

    public string Mail { get; set; } = null!;

    public string Password { get; set; } = null!;

    public decimal Gender { get; set; }

    public decimal Dob { get; set; }

    public string Location { get; set; } = null!;

    public byte[] Pic { get; set; } = null!;

    public string? Bio { get; set; }

    public string? Interests { get; set; }

    public decimal? LastLogin { get; set; }

    public decimal? CreatedAt { get; set; }

    public decimal? UpdatedAt { get; set; }

    public decimal? IsActive { get; set; }

    public decimal? Role { get; set; }
}
