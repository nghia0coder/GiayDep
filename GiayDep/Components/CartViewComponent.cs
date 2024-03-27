using GiayDep.Models;
using GiayDep.Repository;
using GiayDep.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GiayDep.Components
{
   
    public class CartViewComponent : ViewComponent
    {
        private readonly GiaydepContext _context;
        public CartViewComponent(GiaydepContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<CartItemsModel> cartItems = HttpContext.Session.GetJson<List<CartItemsModel>>("Cart") ?? new List<CartItemsModel>();
            Item cart = new()
            {
                CartItems = cartItems,
                Quanity = cartItems.Count(),
<<<<<<< Updated upstream
                Total = cartItems.Sum(x => (x.Quanity ?? 0) * (x.Price ?? 0))
            };
            ViewBag.TongTien = cart.Total;
            ViewBag.TongQuanity = cart.Quanity;
            return View();
        }
        public double TinhTongQuanity()
=======
                Total = cartItems.Sum(x => (x.SoLuong ?? 0) * (x.DonGia ?? 0))
            };
            ViewBag.TongTien = cart.Total;
            ViewBag.TongSoLuong = cart.Quanity;
            return View();
        }
        public double TinhTongSoLuong()
>>>>>>> Stashed changes
        {
      
            List<Item> lstGioHang = LayGioHang();
            if (lstGioHang == null)  
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.Quanity); 
        }
        public List<Item> LayGioHang()
        {
  
            List<Item> cart = HttpContext.Session.GetJson<List<Item>>("Cart");
            if (cart == null)
            {
              
                cart = new List<Item>();
                HttpContext.Session.SetJson("Cart", cart);
            }
            return cart;
        }
        public int? TinhTongTien()
        {
            List<Item> cart = HttpContext.Session.GetJson<List<Item>>("Cart");
            if (cart == null)
            {
                return 0;
            }
            return cart.Sum(n => n.Total);
        }
    }
}
