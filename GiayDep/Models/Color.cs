using System;
using System.Collections.Generic;

namespace GiayDep.Models
{
    public partial class Color
    {
        public Color()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public int Id { get; set; }
        public string? Color1 { get; set; }
        public string? ColorHex { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
