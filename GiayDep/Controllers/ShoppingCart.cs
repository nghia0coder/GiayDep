using GiayDep.Models;
using Microsoft.AspNetCore.Mvc;
using GiayDep.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using GiayDep.Repository;

namespace GiayDep.Controllers
{
    public class ShoppingCart : Controller
    {
        private readonly GiaydepContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShoppingCart (GiaydepContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
       

        public IActionResult Index()
        {
            List<CartItemsModel> cartItems = HttpContext.Session.GetJson<List<CartItemsModel>>("Cart") ?? new List<CartItemsModel>();
            Item cart = new()
            {
                CartItems = cartItems,
                Quanity = cartItems.Count(),
                Total = cartItems.Sum(x => (x.SoLuong ?? 0) * (x.DonGia ?? 0))
            };
            ViewBag.TongTien = cart.Total;
            ViewBag.TongSoLuong = cart.Quanity;
            return View(cart);
        }
        public async Task<IActionResult> ThemGioHang(int MaSP, string strURL)
        {
            SanPham sanPham =  await _context.SanPhams.FindAsync(MaSP);
            List<CartItemsModel> cart = HttpContext.Session.GetJson<List<CartItemsModel>>("Cart") ?? new List<CartItemsModel>();
            CartItemsModel cartItems = cart.Where(c => c.MaSP == MaSP).FirstOrDefault();
            if (cartItems == null)
            {
                cart.Add(new CartItemsModel(sanPham));
            }
            else
            {
                cartItems.SoLuong += 1; // this is increase quanity 
            }
            HttpContext.Session.SetJson("Cart", cart);
            TempData["success"] = "Thêm vào giỏ hàng thành công";
            return Redirect(strURL);
        }
        public async Task<IActionResult> Decrease(int Id)
        {

            List<CartItemsModel> cart =  HttpContext.Session.GetJson<List<CartItemsModel>>("Cart");
            CartItemsModel cartItems =  cart.Where(c => c.MaSP == Id).FirstOrDefault();
            if (cartItems.SoLuong >  1)
            {
                --cartItems.SoLuong;
            }
            else
            {
                cart.RemoveAll(p =>p.MaSP == Id);
            }
            if (cart.Count ==0)
            {
                HttpContext.Session.Remove("Cart");
            }    
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }    
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Increase(int Id)
        {

            List<CartItemsModel> cart = HttpContext.Session.GetJson<List<CartItemsModel>>("Cart");
            CartItemsModel cartItems = cart.Where(c => c.MaSP == Id).FirstOrDefault();
            if (cartItems.SoLuong >= 1)
            {
                cartItems.SoLuong +=1;
            }
            HttpContext.Session.SetJson("Cart", cart);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Removed(int Id)
        {

            List<CartItemsModel> cart = HttpContext.Session.GetJson<List<CartItemsModel>>("Cart");
            cart.RemoveAll(n => n.MaSP == Id);
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            TempData["success"] = "Đã Xóa Sản Phẩm Ra Khỏi Giỏ Hàng";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Clear(int Id)
        {
            TempData["success"] = "Đã Xóa Tất Cả Sản Phẩm Ra Khỏi Giỏ Hàng";
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }
        public IActionResult Success ()
        {
            return View();
        }
        //public IActionResult SuaGioHang(int MaSP)
        //{
        //    // Check if the shopping cart session exists
        //    if (HttpContext.Session.GetJson<List<Item>>("GioHang") == null)
        //    {
        //        return RedirectToAction("Index", "Home");   // Redirect to the home page
        //    }

        //    // Check if the product exists in the database
        //    var sp = _context.SanPhams.SingleOrDefault(n => n.Idsp == MaSP);

        //    if (sp == null)
        //    {
        //        // Return 404 if the product does not exist
        //        return NotFound();
        //    }

        //    // Get the shopping cart from the session
        //    List<Item> lstGioHang = LayGioHang();

        //    // Check if the product exists in the shopping cart
        //    Item spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);

        //    if (spCheck == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    // Pass the shopping cart list to the view
        //    ViewBag.GioHang = lstGioHang;

        //    // If the product exists, return the view with the product details
        //    return View(spCheck);
        //}

        //[HttpPost]
        //public ActionResult CapNhatGioHang(Item itemGH)
        //{
        //    //ktra số lượng tồn sau khi sửa
        //    SanPham spCheck = _context.SanPhams.Single(n => n.Idsp == itemGH.MaSP);
        //    if (spCheck.Soluong < itemGH.SoLuong)
        //    {
        //        return View("ThongBao");
        //    }
        //    //update số lg trong session giỏ hàng
        //    //bc1: Lấy list giỏ hàng từ sesssion giỏ hàng
        //    List<Item> lstGH = LayGioHang();
        //    //bc2: lấy sp cần update từ trong list giỏ hàng
        //    Item itemGHUpdate = lstGH.Find(n => n.MaSP == itemGH.MaSP);  //pt find dùng để tìm các trường mong muốn
        //    //bc3: update lại số lg và thành tiền
        //    itemGHUpdate.SoLuong = itemGH.SoLuong;
        //    itemGHUpdate.ThanhTien = itemGHUpdate.SoLuong * itemGHUpdate.DonGia;

        //    return RedirectToAction("XemGioHang");
        //}
        //public IActionResult XoaGioHang(int MaSP)
        //{
        //    // Check if the shopping cart session exists
        //    if (HttpContext.Session.GetJson<List<Item>>("GioHang") == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    // Check if the product exists in the database
        //    var sp = _context.SanPhams.SingleOrDefault(n => n.Idsp == MaSP);
        //    if (sp == null)
        //    {
        //        // Return 404 if the product does not exist
        //        return NotFound();
        //    }

        //    // Get the shopping cart from the session
        //    List<Item> lstGioHang = LayGioHang();

        //    // Check if the product exists in the shopping cart
        //    Item spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
        //    if (spCheck == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    // Remove the item from the shopping cart
        //    lstGioHang.Remove(spCheck);

        //    return RedirectToAction("XemGioHang");
        //}
        public IActionResult DatHang(int IDtv)
        {
            // Check if the shopping cart session exists
            if (HttpContext.Session.GetJson<List<Item>>("Cart") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Membership tv = HttpContext.Session.GetJson<Membership>("TaiKhoan");
            // Create a new customer
            KhachHang khach = new KhachHang();
            khach.Tenkh = tv.HoTen;
            khach.Diachi = tv.DiaChi;
            khach.Email = tv.Email;
            khach.Sđt = tv.Sđt;
            khach.Gioitinh = tv.Gioitinh;
            khach.Idtv = tv.Idtv;
            _context.KhachHangs.Add(khach);
            _context.SaveChanges();
            // Add a new order
            HoaDon ddh = new HoaDon();
            ddh.Ngaythanhtoan = DateTime.Now;
            ddh.Tinhtranggiaohang = false;
            ddh.Dathanhtoan = false;
            ddh.Khuyenmai = 0;
            ddh.Dahuy = false;
            ddh.Daxoa = false;
            ddh.Makh = khach.Idkhachhang;
            _context.HoaDons.Add(ddh);
            _context.SaveChanges();

            // Add order details
            List<CartItemsModel> cart = HttpContext.Session.GetJson<List<CartItemsModel>>("Cart") ?? new List<CartItemsModel>();
            foreach (var item in cart)
            {
                CtHoaDon ctdh = new CtHoaDon();
                ctdh.Idhoadon = ddh.Idhoadon;
                ctdh.Idsp = item.MaSP;
                ctdh.Tensp = item.TenSP;
                ctdh.Soluong = item.SoLuong;
                ctdh.Dongia = item.DonGia;
                _context.CtHoaDons.Add(ctdh);
            }
            _context.SaveChanges();
            HttpContext.Session.Remove("Cart"); // Clear the shopping cart session

            // Redirect to the order success page
            return RedirectToAction("Success");
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
