using GiayDep.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace GiayDep.Controllers
{
    public class LogoutController : Controller
    {
        private readonly GiaydepContext _context;
        public LogoutController(GiaydepContext context)
        {
            _context = context;
        }
        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear(); // Xóa dữ liệu trong session

            // Đăng xuất người dùng
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index","Home");
        }

    }
}
