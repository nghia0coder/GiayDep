using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiayDep.Models
{
    public partial class ProductItem
    {
        public ProductItem()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
            OrdersDetails = new HashSet<OrdersDetail>();
            ProductImages = new HashSet<ProductImage>();
            ProductVariations = new HashSet<ProductVariation>();
        }

        public int ProductItemsId { get; set; }
        public int ProductId { get; set; }
        public int? ColorId { get; set; }
        public string? ProductCode { get; set; }

        public virtual Color? Color { get; set; }
        public virtual Product? Product { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<OrdersDetail> OrdersDetails { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductVariation> ProductVariations { get; set; }

        [NotMapped]
        public IFormFile Image1 { get; set; }
        [NotMapped]
        public IFormFile Image2 { get; set; }
        [NotMapped]
        public IFormFile Image3 { get; set; }
        [NotMapped]
        public IFormFile Image4 { get; set; }
    }
}
