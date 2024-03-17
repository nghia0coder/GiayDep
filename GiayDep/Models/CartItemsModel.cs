namespace GiayDep.Models
{
    public class CartItemsModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int? Quanity { get; set; }
        public int? Price { get; set; }
        public int? Total
        {
            get { return Price * Quanity; }
        }
        public string Size { get; set; }
        public string HinhAnh { get; set; }

        public string Hang { get; set; }

        public CartItemsModel() { }

        // Constructor theo id (dùng cho trường hợp chỉ có sl=1)
        public CartItemsModel(ProductVariation Product)
        {
            ProductID = Product.ProductItems.ProductId;
            ProductName = Product.ProductItems.Product.ProductName;
            Quanity = Product.QtyinStock;
            Price = Product.ProductItems.Product.Price;
            Quanity = 1;
            //HinhAnh = Product.Hinhanh1;
   
   
        }
    }
}
