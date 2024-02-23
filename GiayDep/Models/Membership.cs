using System;
using System.Collections.Generic;

namespace GiayDep.Models;

public partial class Membership
{
    public int Idtv { get; set; }

    public string? Tk { get; set; }

    public string? Mk { get; set; }

    public string? HoTen { get; set; }

    public string? DiaChi { get; set; }

    public string? Gioitinh { get; set; }

    public string? Email { get; set; }

    public string? Sđt { get; set; }

    public int? MaLoaiTv { get; set; }

    public virtual ICollection<KhachHang> KhachHangs { get; } = new List<KhachHang>();

    public virtual MembershipType? MaLoaiTvNavigation { get; set; }
}
