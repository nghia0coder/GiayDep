using System;
using System.Collections.Generic;

namespace GiayDep.Models;

public partial class HoaDon
{
    public int Idhoadon { get; set; }

    public DateTime? Ngaythanhtoan { get; set; }

    public bool Tinhtranggiaohang { get; set; }

    public DateTime? Ngaygiao { get; set; }

    public bool Dathanhtoan { get; set; }

    public bool? Dahuy { get; set; }

    public bool? Daxoa { get; set; }

    public int Makh { get; set; }

    public int? Khuyenmai { get; set; }

    public virtual ICollection<CtHoaDon> CtHoaDons { get; } = new List<CtHoaDon>();

    public virtual KhachHang MakhNavigation { get; set; } = null!;
}
