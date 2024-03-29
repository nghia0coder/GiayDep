﻿using GiayDep.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;


namespace GiayDep.Controllers
{	
	
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GiaydepContext _context;
       

        public HomeController(ILogger<HomeController> logger, GiaydepContext context)
        {
            _logger = logger;
            _context = context;
        }

		public ActionResult Index()
		{
			//Lần lượt tạo các viewbag để lấy list sp từ csdl
			//List CategoryId bằng 1
			var lstLTM = _context.ProductItems
				.Include(n => n.Product.Category)
				.Include(n => n.ProductVariations)
				.OrderBy(n => Guid.NewGuid())
				.ToList();

			//Gán vào viewbag
			ViewBag.ListLTM = lstLTM;

			var lstSelling = _context.ProductItems
				.Where(n => n.ProductId == 2)
				.Include(n => n.Product.Category)
				.Include(n => n.ProductVariations)
				.ToList();
			//Gán vào viewbag
			ViewBag.ListSelling = lstSelling;

			//List CategoryId bằng 3
			var lstDTM = _context.ProductItems
				.Include(n => n.Product.Category)
				.Include(n => n.ProductVariations)
				.Include(n => n.Product.BrandNavigation)
				.OrderBy(n => Guid.NewGuid())
				.ToList();
			//Gán vào viewbag
			ViewBag.ListDTM = lstDTM;


			return View();
		}
		public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
