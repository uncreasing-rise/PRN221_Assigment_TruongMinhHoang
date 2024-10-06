using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class SystemAccount
{
    public int AccountId { get; set; }

    public string AccountPassword { get; set; } = null!;

    public string AccountFullName { get; set; } = null!;

    public string? AccountEmail { get; set; }

    public int? Role { get; set; }
}
