﻿using GiayDep.Models;
using Microsoft.AspNetCore.Mvc;
using GiayDep.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using GiayDep.Repository;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
                Total = cartItems.Sum(x => (x.Quanity ?? 0) * (x.Price ?? 0))
            };
            ViewBag.TongTien = cart.Total;
            ViewBag.TongQuanity = cart.Quanity;
            return View(cart);
        }
        public async Task<IActionResult> ThemGioHang(int MaSP,int? size, string strURL)
        {
            ProductVariation Product =  await _context.ProductVariations
                .Include(n => n.ProductItems.Product)
                .FirstOrDefaultAsync(s => s.ProductItemsId == MaSP &&  s.SizeId == size);
            List<CartItemsModel> cart = HttpContext.Session.GetJson<List<CartItemsModel>>("Cart") ?? new List<CartItemsModel>();
            CartItemsModel cartItems = cart.Where(c => c.ProductID == MaSP).FirstOrDefault();
            if (cartItems == null)
            {
                cart.Add(new CartItemsModel(Product));
            }
            else
            {
                cartItems.Quanity += 1; // this is increase quanity 
            }
            HttpContext.Session.SetJson("Cart", cart);
            TempData["success"] = "Thêm vào giỏ hàng thành công";
            return Redirect(strURL);
        }
        public async Task<IActionResult> Decrease(int Id)
        {

            List<CartItemsModel> cart =  HttpContext.Session.GetJson<List<CartItemsModel>>("Cart");
            CartItemsModel cartItems =  cart.Where(c => c.ProductID == Id).FirstOrDefault();
            if (cartItems.Quanity >  1)
            {
                --cartItems.Quanity;
            }
            else
            {
                cart.RemoveAll(p =>p.ProductID == Id);
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
            CartItemsModel cartItems = cart.Where(c => c.ProductID == Id).FirstOrDefault();
            if (cartItems.Quanity >= 1)
            {
                cartItems.Quanity +=1;
            }
            HttpContext.Session.SetJson("Cart", cart);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Removed(int Id)
        {

            List<CartItemsModel> cart = HttpContext.Session.GetJson<List<CartItemsModel>>("Cart");
            cart.RemoveAll(n => n.ProductID == Id);
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
        public IActionResult DatHang()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Check if the shopping cart session exists

            // Add a new order
            Order ddh = new Order();
            ddh.OrderDate = DateTime.Now;
            ddh.Delivered = false;
            ddh.Status = false;
            ddh.CustomerId = userId;
            _context.Orders.Add(ddh);
            _context.SaveChanges();

            // Add order details
            List<CartItemsModel> cart = HttpContext.Session.GetJson<List<CartItemsModel>>("Cart") ?? new List<CartItemsModel>();
            foreach (var item in cart)
            {
                OrdersDetail ctdh = new OrdersDetail();
                ctdh.OrderId = ddh.OrderId;
                ctdh.ProductVar.ProductItems.Product.ProductId = item.ProductID;
                ctdh.Quanity = item.Quanity;
                ctdh.Price = item.Price;
                _context.OrdersDetails.Add(ctdh);
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
