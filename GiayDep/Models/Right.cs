using System;
using System.Collections.Generic;

namespace GiayDep.Models;

public partial class Right
{
    public string Idright { get; set; } = null!;

    public string? NameRight { get; set; }

    public virtual ICollection<MembershipRight> MembershipRights { get; } = new List<MembershipRight>();
}
