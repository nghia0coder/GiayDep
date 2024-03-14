using System;
using System.Collections.Generic;

namespace GiayDep.Models
{
    public partial class ChitietSanPham
    {
        public int Idsp { get; set; }
        public int Idcolor { get; set; }
        public int Idsize { get; set; }

        public virtual Color IdcolorNavigation { get; set; } = null!;
        public virtual Size IdsizeNavigation { get; set; } = null!;
        public virtual SanPham IdspNavigation { get; set; } = null!;
    }
}
