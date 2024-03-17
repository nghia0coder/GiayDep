﻿using System;
using System.Collections.Generic;

namespace GiayDep.Models
{
    public partial class Suppiler
    {
        public Suppiler()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
