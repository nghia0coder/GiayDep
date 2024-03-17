using System;
using System.Collections.Generic;

namespace GiayDep.Models
{
    public partial class InvoiceDetail
    {
        public int ProductId { get; set; }
        public int InvoiceId { get; set; }
        public int? Quanity { get; set; }
        public int? Price { get; set; }

        public virtual Invoice Invoice { get; set; } = null!;
        public virtual ProductItem Product { get; set; } = null!;
    }
}
