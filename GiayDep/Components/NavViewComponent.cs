using GiayDep.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiayDep.Components
{
    public class NavViewComponent : ViewComponent
    {
        private GiaydepContext _context;
        public NavViewComponent (GiaydepContext context)
        {
            _context = context;
        }
		public IViewComponentResult Invoke()
		{
			var listCategory = _context.Categories
				.ToList();
			return View(listCategory);
		}
	}
}
