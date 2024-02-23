using System;
using System.Collections.Generic;

namespace GiayDep.Models;

public partial class PhieuNhap
{
    public int Idphieunhap { get; set; }

    public DateTime Ngaynhap { get; set; }

    public int Idnhacc { get; set; }

    public virtual ICollection<CtPhieuNhap> CtPhieuNhaps { get; } = new List<CtPhieuNhap>();

    public virtual NhaCungCap IdnhaccNavigation { get; set; } = null!;
}
