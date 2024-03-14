using System;
using System.Collections.Generic;

namespace GiayDep.Models
{
    public partial class SoLuongTon
    {
        public int Idsp { get; set; }
        public int? Soluongton1 { get; set; }

        public virtual SanPham IdspNavigation { get; set; } = null!;
    }
}
