using System;
using System.Collections.Generic;

namespace TeachifyBE_Data.Entities;

public partial class TblUser
{
    public Guid Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
}
