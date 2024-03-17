using System;
using System.Collections.Generic;

namespace GiayDep.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public int InvoiceId { get; set; }
        public DateTime Status { get; set; }
        public int SupplierId { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Suppiler Supplier { get; set; } = null!;
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
