using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiayDep.Models;
using Microsoft.AspNetCore.Authorization;

namespace GiayDep.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupplierController : Controller
    {
        private readonly GiaydepContext _context;

        public SupplierController(GiaydepContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Manager")]
        public IActionResult Index()
        {
            var Suppilers = _context.Suppilers.ToList();
            return View(Suppilers);
        }

        // GET: Admin/NhaCC/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Suppiler = await _context.Suppilers.FindAsync(id);

            if (Suppiler == null)
            {
                return NotFound();
            }

            return View(Suppiler);
        }
        [Authorize(Roles = "Manager")]
        // GET: Admin/NhaCC/Create
        public async Task<IActionResult> Create()
        {   var sxList = _context.Suppilers.ToList();
            ViewData["BrandId"] = new SelectList(sxList, "BrandId", "BrandId");
            return View();
        }
        [Authorize(Roles = "Manager")]
        // POST: Admin/NhaCC/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierId,SupplierName,Address,Phone,Email,BrandId")] Suppiler _Suppiler)
        {
            
                _context.Suppilers.Add(_Suppiler);
                return RedirectToAction(nameof(Index));
          
        }
        [Authorize(Roles = "Manager")]
        // GET: Admin/NhaCC/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Suppiler = await _context.Suppilers.FindAsync(id);

            if (Suppiler == null)
            {
                return NotFound();
            }
            var sxList = _context.Suppilers.ToList();
            ViewData["BrandId"] = new SelectList(sxList, "BrandId", "BrandId");
            return View(Suppiler);
        }
        [Authorize(Roles = "Manager")]
        // POST: Admin/NhaCC/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierId,SupplierName,Address,Phone,Email")] Suppiler _Suppiler)
        {
            if (id != _Suppiler.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(_Suppiler);
                return RedirectToAction(nameof(Index));
            }
            return View(_Suppiler);
        }
        [Authorize(Roles = "Manager")]
        // GET: Admin/NhaCC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Suppiler = await _context.Suppilers.FindAsync(id);

            if (Suppiler == null)
            {
                return NotFound();
            }

            return View(Suppiler);
        }
        [Authorize(Roles = "Manager")]
        // POST: Admin/NhaCC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = _context.Suppilers.FindAsync(id);
            if (supplier != null)
            {
                _context.Remove(supplier);
            }    
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id)
        {
            return (_context.Suppilers?.Any(e => e.SupplierId == id)).GetValueOrDefault();
        }
    }
}
