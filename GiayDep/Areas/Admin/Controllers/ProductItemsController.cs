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
        public async Task<IActionResult> Index()
        {
            var giaydepContext = _context.ProductItems.Include(p => p.Color).Include(p => p.Product);
            return View(await giaydepContext.ToListAsync());
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
            ViewData["productImg"] = await _context.ProductImages
                .Where(n => n.ProductItemsId == productItem.ProductItemsId)
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
            var productItem = new ProductItem()
            {
                ProductId = product.ProductId,
                ProductCode = product.ProductCode,
                ColorId = product.ColorId,
            };
            _context.ProductItems.Add(productItem);
            await _context.SaveChangesAsync();


            for (int i = 0; i<product.Image.Count(); i ++)
            {
                string uniqueFileName = GetProfilePhotoFileName(product,i);

                var productImg = new ProductImage()
                {
                    ProductItemsId = productItem.ProductItemsId,
                    ImageUrl = uniqueFileName
                };
                _context.ProductImages.Add(productImg);
            }    
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
        private string GetProfilePhotoFileName(ProductItemViewModel Product,int i)
        {
            string uniqueFileName = null;

            if (Product.Image[i] != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "Contents/img/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Product.Image[i].FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Image[i].CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
