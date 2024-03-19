﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiayDep.Models;
using Microsoft.AspNetCore.Hosting;


namespace GiayDep.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly GiaydepContext _context;
        private readonly IWebHostEnvironment _webHost;

        public ProductsController(GiaydepContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }
        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var GiaydepContext = _context.Products
                .Include(n => n.Category)
                .Include(n => n.BrandNavigation)
                .ToList();
            return View(GiaydepContext);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var Product = await _context.Products
                .Include(_ => _.Category)
                .Include(_ => _.BrandNavigation)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (Product == null)
            {
                return NotFound();
            }

            return View(Product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["Brand"] = new SelectList(_context.Brands, "BrandId", "BrandName");
            Product Product = new Product();
            return View(Product);
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Product Product)
        {

       

            _context.Products.Add(Product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
           
        }


        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var Product = await _context.Products.FindAsync(id);
            if (Product == null)
            {
                return NotFound();
            }

            //TempData["img1"] = Product.Hinhanh1;
            //TempData["img2"] = Product.Hinhanh2;
            //TempData["img3"] = Product.Hinhanh3;
            //TempData["img4"] = Product.Hinhanh4;

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", Product.CategoryId);
            ViewData["Brand"] = new SelectList(_context.Suppilers, "SupplierId", "SupplierId", Product.Brand);
	
		
			return View(Product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product Product)
        {
            if (id != Product.ProductId)
            {
                return NotFound();
            }

           
             //string uniqueFileName1 = GetProfilePhotoFileName1(Product);
             //Product.Hinhanh1 = uniqueFileName1;
             //string uniqueFileName2 = GetProfilePhotoFileName2(Product);
             //Product.Hinhanh2 = uniqueFileName2;
             //string uniqueFileName3 = GetProfilePhotoFileName3(Product);
             //Product.Hinhanh3 = uniqueFileName3;
             //string uniqueFileName4 = GetProfilePhotoFileName4(Product);
             //Product.Hinhanh4 = uniqueFileName4;

             _context.Update(Product);
             _context.SaveChanges();
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", Product.CategoryId);
            ViewData["Brand"] = new SelectList(_context.Suppilers, "SupplierId", "SupplierId", Product.Brand);
          


            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var Product = await _context.Products
         
                .Include(s => s.BrandNavigation)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (Product == null)
            {
                return NotFound();
            }

            return View(Product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'GiaydepContext.Products'  is null.");
            }
            var Product = await _context.Products.FindAsync(id);
            if (Product != null)
            {
                _context.Products.Remove(Product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private string GetProfilePhotoFileName1(ProductItem Product)
        {
            string uniqueFileName = null;

            if (Product.Image1 != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Contents/img/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Product.Image1.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Image1.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string GetProfilePhotoFileName2(ProductItem Product)
        {
            string uniqueFileName = null;

            if (Product.Image2 != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Contents/img/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Product.Image2.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Image2.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string GetProfilePhotoFileName3(ProductItem Product)
        {
            string uniqueFileName = null;

            if (Product.Image3 != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Contents/img/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Product.Image3.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Image3.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string GetProfilePhotoFileName4(ProductItem Product)
        {
            string uniqueFileName = null;

            if (Product.Image4 != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Contents/img/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Product.Image4.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Image4.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
