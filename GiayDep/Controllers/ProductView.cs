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
   
        public ActionResult Productstyle1Partial()
        {
            return PartialView();
        }
        public ActionResult Productstyle2Partial()
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
            var sp = await _context.Products
                .Include(n => n.Category)
     
     
         
                .SingleOrDefaultAsync(n => n.ProductId == id);


            ViewBag.ListSP = _context.Products
                .Where(n => n.CategoryId == sp.CategoryId);

            if (sp == null)
            {
                return NotFound();
            }

            return View(sp);
        }

		[Route("Product/{slug}-{id:int}")]
		public IActionResult ProductCate(int? Id)
		{
			// Check if the parameter is null
			if (Id == null)
			{
				return BadRequest();
			}

			// Load products based on the specified criteria
			var lstSP = _context.Products
				.Where(n => n.BrandNavigation.BrandId == Id)
				.Include(n => n.Category)
                .GroupBy(n => n.ProductName)
                .Select(n => n.FirstOrDefault())
                .ToList();

			// Check if there are any products
			if (lstSP.Count() == 0)
			{
				return NotFound();
			}
			ViewBag.CategoryId = Id;

			// Return the view with paginated products
			return View(lstSP.OrderBy(n => n.ProductId));
		}
	}
}
