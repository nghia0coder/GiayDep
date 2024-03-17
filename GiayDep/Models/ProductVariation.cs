using System;
using System.Collections.Generic;

namespace GiayDep.Models
{
    public partial class ProductVariation
    {
        public int VariationId { get; set; }
        public int ProductItemsId { get; set; }
        public int? SizeId { get; set; }
        public int? QtyinStock { get; set; }

        public virtual ProductItem? ProductItems { get; set; }
        public virtual Size? Size { get; set; }
    }
}
