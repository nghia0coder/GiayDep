using GiayDep.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GiayDep.Controllers
{
    public class ProductView : Controller
    {
        private readonly GiaydepContext _context;

        public ProductView (GiaydepContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
   
        public ActionResult SanPhamStyle1Partial()
        {
            return PartialView();
        }
        public ActionResult SanPhamStyle2Partial()
        {
            return PartialView();
        }
        [Route("post/{slug}-{id:int}")]
        public async Task<IActionResult> XemChiTiet(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var sp = await _context.SanPhams
                .Include(n => n.MaloaispNavigation)
                .Include(n => n.SizeNavigation)
                .Include(n => n.ManhasxNavigation)
                .SingleOrDefaultAsync(n => n.Idsp == id);
            ViewBag.ListSP = _context.SanPhams
                .Where(n => n.Maloaisp == sp.Maloaisp);


            var sizes = _context.SanPhams.Where(n => n.Tensp == sp.Tensp).Include(s => s.SizeNavigation).ToList();
                
            ViewBag.ListSizes = sizes;

            if (sp == null)
            {
                return NotFound();
            }

            return View(sp);
        }

		[Route("sanpham/{slug}-{id:int}")]
		public IActionResult ProductCate(int? Id)
		{
			// Check if the parameter is null
			if (Id == null)
			{
				return BadRequest();
			}

			// Load products based on the specified criteria
			var lstSP = _context.SanPhams
				.Where(n => n.Manhasx == Id)
				.Include(n => n.MaloaispNavigation)
                .GroupBy(n => n.Tensp)
                .Select(n => n.FirstOrDefault())
                .ToList();

			// Check if there are any products
			if (lstSP.Count() == 0)
			{
				return NotFound();
			}
			ViewBag.MaLoaiSP = Id;

			// Return the view with paginated products
			return View(lstSP.OrderBy(n => n.Idsp));
		}
	}
}
