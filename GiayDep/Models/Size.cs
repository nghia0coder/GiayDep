using System;
using System.Collections.Generic;

namespace GiayDep.Models
{
    public partial class Size
    {
        public Size()
        {
            ProductVariations = new HashSet<ProductVariation>();
        }

        public int SizeId { get; set; }
        public int? Size1 { get; set; }

        public virtual ICollection<ProductVariation> ProductVariations { get; set; }
    }
}
