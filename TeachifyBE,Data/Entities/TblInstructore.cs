﻿using System;
using System.Collections.Generic;

namespace TeachifyBE_Data.Entities;

public partial class TblInstructore
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Language { get; set; }

    public string? Nationality { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Education { get; set; }

    public string? OneLineTitle { get; set; }

    public string? Description { get; set; }

    public string? Experience { get; set; }

    public string? HourlyRate { get; set; }

    public string? CourseDomain { get; set; }

    public string? City { get; set; }

    public string? ImageArray { get; set; }
}
