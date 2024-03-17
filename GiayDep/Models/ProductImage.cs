using System;
using System.Collections.Generic;

namespace GiayDep.Models
{
    public partial class ProductImage
    {
        public int ImageId { get; set; }
        public int? ProductItemsId { get; set; }
        public string? ImageUrl { get; set; }

        public virtual ProductItem? ProductItems { get; set; }
    }
}
