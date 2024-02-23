using System;
using System.Collections.Generic;

namespace GiayDep.Models;

public partial class MembershipType
{
    public int MaLoaiTv { get; set; }

    public string? TenLoai { get; set; }

    public int? KhuyenMai { get; set; }

    public virtual ICollection<MembershipRight> MembershipRights { get; } = new List<MembershipRight>();

    public virtual ICollection<Membership> Memberships { get; } = new List<Membership>();
}
