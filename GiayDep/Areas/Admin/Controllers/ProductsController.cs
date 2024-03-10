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
            var giaydepContext = _context.SanPhams.Include(s => s.MaloaispNavigation).Include(s => s.ManhaccNavigation);
            return View(await giaydepContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SanPhams == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.MaloaispNavigation)
                .Include(s => s.ManhaccNavigation)
                .Include(s => s.ManhasxNavigation)
                .Include(s => s.ColorNavigation)
                .Include(s => s.SizeNavigation)
                .FirstOrDefaultAsync(m => m.Idsp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["Maloaisp"] = new SelectList(_context.LoaiSps, "Idloai", "Idloai");
            ViewData["Manhacc"] = new SelectList(_context.NhaCungCaps, "Idnhacc", "Idnhacc");
            ViewData["Manhasx"] = new SelectList(_context.NhaSanXuats, "Idnhasx", "Tennhasx");
            ViewData["Size"] = new SelectList(_context.Sizes, "Id", "Size1");
            ViewData["Color"] = new SelectList(_context.Color, "Id", "Color1");
            SanPham sanPham = new SanPham();
            return View(sanPham);
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(SanPham sanPham)
        {
            
                string uniqueFileName1 = GetProfilePhotoFileName1(sanPham);
                sanPham.Hinhanh1 = uniqueFileName1;
                string uniqueFileName2 = GetProfilePhotoFileName2(sanPham);
                sanPham.Hinhanh2 = uniqueFileName2;
                string uniqueFileName3 = GetProfilePhotoFileName3(sanPham);
                sanPham.Hinhanh3 = uniqueFileName3;
                string uniqueFileName4 = GetProfilePhotoFileName4(sanPham);
                sanPham.Hinhanh4 = uniqueFileName4;

                _context.SanPhams.Add(sanPham);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
           
        }


        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SanPhams == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }

            TempData["img1"] = sanPham.Hinhanh1;
            TempData["img2"] = sanPham.Hinhanh2;
            TempData["img3"] = sanPham.Hinhanh3;
            TempData["img4"] = sanPham.Hinhanh4;

            ViewData["Maloaisp"] = new SelectList(_context.LoaiSps, "Idloai", "Idloai", sanPham.Maloaisp);
            ViewData["Manhacc"] = new SelectList(_context.NhaCungCaps, "Idnhacc", "Idnhacc", sanPham.Manhacc);
			ViewData["Manhasx"] = new SelectList(_context.NhaSanXuats, "Idnhasx", "Tennhasx",sanPham.Manhasx);
			ViewData["Size"] = new SelectList(_context.Sizes, "Id", "Size1",sanPham.Size);
			ViewData["Color"] = new SelectList(_context.Color, "Id", "Color1",sanPham.Color);
			return View(sanPham);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SanPham sanPham)
        {
            if (id != sanPham.Idsp)
            {
                return NotFound();
            }
            if (sanPham.Hinhanh1 == null) 
            {
                sanPham.Hinhanh1 = TempData["img1"]?.ToString();
            }
            else
            {
                string uniqueFileName1 = GetProfilePhotoFileName1(sanPham);
                sanPham.Hinhanh1 = uniqueFileName1;
            }
            if (sanPham.Hinhanh2 == null)
            {
                sanPham.Hinhanh2 = TempData["img2"]?.ToString();
            }
            else
            {
                string uniqueFileName2 = GetProfilePhotoFileName2(sanPham);
                sanPham.Hinhanh2 = uniqueFileName2;
            }
            if (sanPham.Hinhanh3 == null)
            {
                sanPham.Hinhanh3 = TempData["img3"]?.ToString();
            }
            else
            {
                string uniqueFileName3 = GetProfilePhotoFileName1(sanPham);
                sanPham.Hinhanh3 = uniqueFileName3;
            }
            if (sanPham.Hinhanh4 == null)
            {
                sanPham.Hinhanh4 = TempData["img4"]?.ToString();
            }
            else
            {
                string uniqueFileName4 = GetProfilePhotoFileName1(sanPham);
                sanPham.Hinhanh4 = uniqueFileName4;
            }
         
            _context.Update(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SanPhams == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.MaloaispNavigation)
                .Include(s => s.ManhaccNavigation)
                .FirstOrDefaultAsync(m => m.Idsp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SanPhams == null)
            {
                return Problem("Entity set 'GiaydepContext.SanPhams'  is null.");
            }
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPhams.Remove(sanPham);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private string GetProfilePhotoFileName1(SanPham sanPham)
        {
            string uniqueFileName = null;

            if (sanPham.Image1 != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Contents/img/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + sanPham.Image1.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    sanPham.Image1.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string GetProfilePhotoFileName2(SanPham sanPham)
        {
            string uniqueFileName = null;

            if (sanPham.Image2 != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Contents/img/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + sanPham.Image2.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    sanPham.Image2.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string GetProfilePhotoFileName3(SanPham sanPham)
        {
            string uniqueFileName = null;

            if (sanPham.Image3 != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Contents/img/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + sanPham.Image3.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    sanPham.Image3.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string GetProfilePhotoFileName4(SanPham sanPham)
        {
            string uniqueFileName = null;

            if (sanPham.Image4 != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Contents/img/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + sanPham.Image4.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    sanPham.Image4.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        private bool SanPhamExists(int id)
        {
          return (_context.SanPhams?.Any(e => e.Idsp == id)).GetValueOrDefault();
        }
    }
}
