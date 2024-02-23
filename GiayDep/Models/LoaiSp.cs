using System;
using System.Collections.Generic;

namespace GiayDep.Models;

public partial class LoaiSp
{
    public int Idloai { get; set; }

    public string? Tenloai { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; } = new List<SanPham>();
}
