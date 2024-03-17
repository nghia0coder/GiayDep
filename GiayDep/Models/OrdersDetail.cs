using System;
using System.Collections.Generic;

namespace GiayDep.Models
{
    public partial class OrdersDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int? Price { get; set; }
        public int? Quanity { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual ProductItem Product { get; set; } = null!;
    }
}
