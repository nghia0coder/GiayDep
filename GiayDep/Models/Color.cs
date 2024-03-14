using System;
using System.Collections.Generic;

namespace GiayDep.Models
{
    public partial class Color
    {
        public Color()
        {
            ChitietSanPhams = new HashSet<ChitietSanPham>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<ChitietSanPham> ChitietSanPhams { get; set; }
    }
}
