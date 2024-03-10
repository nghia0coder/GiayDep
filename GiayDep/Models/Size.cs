using System;
using System.Collections.Generic;

namespace GiayDep.Models
{
    public partial class Size
    {
        public Size()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public int Id { get; set; }
        public string? Size1 { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
