using GiayDep.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GiayDep.Controllers
{
    public class RegisterController : Controller
    {
        private readonly GiaydepContext _context;
        public RegisterController (GiaydepContext context)
        { 
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(Membership tv)
        {

            try
            {
                tv.MaLoaiTv = 3;
                ViewBag.SuccessMessage = "Đăng ký thành công!. Vui Lòng Đăng Nhập";
                _context.Memberships.Add(tv);
                _context.SaveChanges();
                ViewBag.RedirectDelay = 1500; // milliseconds (2 seconds)
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi trong quá trình đăng ký.";
            }
            return View(tv);
        }

    }
}
