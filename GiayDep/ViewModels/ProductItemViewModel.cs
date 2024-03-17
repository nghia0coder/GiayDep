using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace GiayDep.ViewModels
{
    public class ProductItemViewModel
    {
        public int ProductItemsId { get; set; }
        public string ProductCode { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        
        public int Size { get; set; }
        public int ProductId { get; set; }
        public IFormFile Image1 { get; set; }

    }
}
