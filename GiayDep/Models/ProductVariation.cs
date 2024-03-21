using System;
using System.Collections.Generic;

namespace GiayDep.Models
{
    public partial class ProductVariation
    {
        public ProductVariation()
        {
            OrdersDetails = new HashSet<OrdersDetail>();
        }

        public int ProductVarId { get; set; }
        public int ProductItemsId { get; set; }
        public int SizeId { get; set; }
        public int? QtyinStock { get; set; }

        public virtual ProductItem ProductItems { get; set; } = null!;
        public virtual Size Size { get; set; } = null!;
        public virtual InvoiceDetail? InvoiceDetail { get; set; }
        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
    }
}
