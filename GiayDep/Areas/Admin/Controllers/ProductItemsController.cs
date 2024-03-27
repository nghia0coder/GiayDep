using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiayDep.Models;
using Microsoft.AspNetCore;
using GiayDep.ViewModels;
using X.PagedList;
using X.PagedList.Mvc.Core;
using X.PagedList.Mvc.Bootstrap4;

namespace GiayDep.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductItemsController : Controller
    {
        private readonly GiaydepContext _context;
        private readonly IWebHostEnvironment _webHost;

        public ProductItemsController(GiaydepContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        // GET: Admin/ProductItems
        public async Task<IActionResult> Index(int page =1)
        {
            page = page < 1? 1 : page;
            int pagesize = 10;

            var giaydepContext = _context.ProductItems.Include(p => p.Color).Include(p => p.Product).ToPagedList(page,pagesize);
            return View(giaydepContext);
        }

        // GET: Admin/ProductItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductItems == null)
            {
                return NotFound();
            }

            var productItem = await _context.ProductItems
                .Include(p => p.Color)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductItemsId == id);

            ViewData["productSize"] = await _context.ProductVariations
                .Where(n => n.ProductItemsId == productItem.ProductItemsId)
                .Include(n => n.Size)
                .ToListAsync();
            

            if (productItem == null)
            {
                return NotFound();
            }

            return View(productItem);
        }

        // GET: Admin/ProductItems/Create
        public IActionResult Create(int? id)
        {
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorName");
            var product = _context.Products.Find(id);
            ViewData["ProductName"] = product.ProductName;
            ViewData["ProductId"] = id;
            ViewData["Size"] = _context.Sizes.ToList();
            return View();
        }

        // POST: Admin/ProductItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductItemViewModel product, List<int> SelectedSizes)
        {

            string uniqueFileName1 = GetProfilePhotoFileName1(product);
            string uniqueFileName2 = GetProfilePhotoFileName2(product);
            string uniqueFileName3 = GetProfilePhotoFileName3(product);
            string uniqueFileName4 = GetProfilePhotoFileName4(product);
     
            var productItem = new ProductItem()
            {
                ProductId = product.ProductId,
                ProductCode = product.ProductCode,
                ColorId = product.ColorId,
                Image1 = uniqueFileName1,
                Image2 = uniqueFileName2,
                Image3 = uniqueFileName3,
                Image4 = uniqueFileName4

            };
            _context.ProductItems.Add(productItem);
            await _context.SaveChangesAsync();

            

            await _context.SaveChangesAsync();

            foreach (var sizeId in SelectedSizes)
            {
                var productSize = new ProductVariation()
                {
                    ProductItemsId = productItem.ProductItemsId,
                    SizeId = sizeId, // Sử dụng sizeId từ danh sách SelectedSizes
                    QtyinStock = 0
                };
                _context.ProductVariations.Add(productSize);
            }
       
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/ProductItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductItems == null)
            {
                return NotFound();
            }

            var productItem = await _context.ProductItems.FindAsync(id);
            if (productItem == null)
            {
                return NotFound();
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorId", productItem.ColorId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productItem.ProductId);
            return View(productItem);
        }

        // POST: Admin/ProductItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductItemsId,ProductId,ColorId,ProductCode")] ProductItem productItem)
        {
            if (id != productItem.ProductItemsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductItemExists(productItem.ProductItemsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "ColorId", "ColorId", productItem.ColorId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", productItem.ProductId);
            return View(productItem);
        }

        // GET: Admin/ProductItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductItems == null)
            {
                return NotFound();
            }

            var productItem = await _context.ProductItems
                .Include(p => p.Color)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductItemsId == id);
            if (productItem == null)
            {
                return NotFound();
            }

            return View(productItem);
        }

        // POST: Admin/ProductItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductItems == null)
            {
                return Problem("Entity set 'GiaydepContext.ProductItems'  is null.");
            }
            var productItem = await _context.ProductItems.FindAsync(id);
            if (productItem != null)
            {
                _context.ProductItems.Remove(productItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductItemExists(int id)
        {
          return (_context.ProductItems?.Any(e => e.ProductItemsId == id)).GetValueOrDefault();
        }
        private string GetProfilePhotoFileName1(ProductItemViewModel Product)
        {
            string uniqueFileName = null;

            if (Product.Img1 != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Contents/img/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Product.Img1.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Img1.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string GetProfilePhotoFileName2(ProductItemViewModel Product)
        {
            string uniqueFileName = null;

            if (Product.Img2 != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Contents/img/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Product.Img2.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Img2.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string GetProfilePhotoFileName3(ProductItemViewModel Product)
        {
            string uniqueFileName = null;

            if (Product.Img3 != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Contents/img/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Product.Img3.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Img3.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        private string GetProfilePhotoFileName4(ProductItemViewModel Product)
        {
            string uniqueFileName = null;

            if (Product.Img4 != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Contents/img/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Product.Img4.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Img4.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
