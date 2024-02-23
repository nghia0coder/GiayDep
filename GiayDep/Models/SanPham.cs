using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiayDep.Models;

public partial class SanPham
{
    public int Idsp { get; set; }

    public string Tensp { get; set; } = null!;

    public int? Dongia { get; set; }

    public int? Soluong { get; set; }

    public string? Baohanh { get; set; }

    public string? Khuyenmai { get; set; }

    public int Maloaisp { get; set; }

    public int? Manhacc { get; set; }

    public string? Hinhanh1 { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<CtHoaDon> CtHoaDons { get; } = new List<CtHoaDon>();

    public virtual ICollection<CtPhieuNhap> CtPhieuNhaps { get; } = new List<CtPhieuNhap>();

    public virtual LoaiSp MaloaispNavigation { get; set; } = null!;

    public virtual NhaCungCap? ManhaccNavigation { get; set; }
    [NotMapped]
    public IFormFile Image {  get; set; }
}
