using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiayDep.Models;

namespace GiayDep.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly GiaydepContext _context;

        public OrderController(GiaydepContext context)
        {
            _context = context;
        }

        public IActionResult ChuaThanhToan()
        {
            var lst = _context.Orders.Include(n => n.CustomerId).Where(n => !n.Status).OrderBy(n => n.OrderDate).ToList();
            return View(lst);
        }

        public IActionResult ChuaGiao()
        {
            var lstDSDHCG = _context.Orders
                .Include(n => n.CustomerId)
                .Where(n => !n.Delivered && n.Status)
                .OrderBy(n => n.Delivered)
                .ToList();
            return View(lstDSDHCG);
        }

        public IActionResult DaGiaoStatus()
        {
            var lstDSDHCG = _context.Orders
                .Include(n => n.CustomerId)
                .Where(n => n.Delivered && n.Status)
                .OrderBy(n => n.DeliveryDate)
                .ToList();
            return View(lstDSDHCG);
        }

        [HttpGet]
        public IActionResult DuyetDonHang(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            Order model = _context.Orders
                .Include(n => n.CustomerId)
                .SingleOrDefault(n => n.OrderId == id);

            if (model == null)
            {
                return NotFound();
            }

            var lstChiTietDH = _context.OrdersDetails
                .Where(n => n.OrderId == id)
                .ToList();

            ViewBag.ListChiTietDH = lstChiTietDH;
            ViewBag.TenKH = model.Customer.UserName;

            return View(model);
        }

        [HttpPost]
        public IActionResult DuyetDonHang(Order ddh)
        {
            Order ddhUpdate = _context.Orders.Single(n => n.OrderId == ddh.OrderId);
            ddhUpdate.Status = ddh.Status;
            ddhUpdate.Delivered = ddh.Delivered;
            _context.SaveChanges();

            var lstChiTietDH = _context.OrdersDetails
                .Where(n => n.OrderId == ddh.OrderId)
                .Include(n => n.ProductVar)
                .ToList();

            ViewBag.ListChiTietDH = lstChiTietDH;

            return View(ddhUpdate);
        }

        // Dispose method
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
