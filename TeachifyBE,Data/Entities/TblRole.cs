using System;
using System.Collections.Generic;

namespace TeachifyBE_Data.Entities;

public partial class TblRole
{
    public Guid Id { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}
