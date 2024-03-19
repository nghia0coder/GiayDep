using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiayDep.Models;

namespace GiayDep.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SuppilersController : Controller
    {
        private readonly GiaydepContext _context;

        public SuppilersController(GiaydepContext context)
        {
            _context = context;
        }

        // GET: Admin/Suppilers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Suppilers.ToListAsync());
        }

        // GET: Admin/Suppilers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            var suppiler = await _context.Suppilers
                .FirstOrDefaultAsync(m => m.SupplierId == id);
            if (suppiler == null)
            {
                return NotFound();
            }

            return View(suppiler);
        }

        // GET: Admin/Suppilers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Suppilers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierId,SupplierName,Address,Phone,Email")] Supplier suppiler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suppiler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suppiler);
        }

        // GET: Admin/Suppilers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Suppilers == null)
            {
                return NotFound();
            }

            var suppiler = await _context.Suppilers.FindAsync(id);
            if (suppiler == null)
            {
                return NotFound();
            }
            return View(suppiler);
        }

        // POST: Admin/Suppilers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierId,SupplierName,Address,Phone,Email")] Supplier suppiler)
        {
            if (id != suppiler.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suppiler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuppilerExists(suppiler.SupplierId))
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
            return View(suppiler);
        }

        // GET: Admin/Suppilers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Suppilers == null)
            {
                return NotFound();
            }

            var suppiler = await _context.Suppilers
                .FirstOrDefaultAsync(m => m.SupplierId == id);
            if (suppiler == null)
            {
                return NotFound();
            }

            return View(suppiler);
        }

        // POST: Admin/Suppilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Suppilers == null)
            {
                return Problem("Entity set 'GiaydepContext.Suppilers'  is null.");
            }
            var suppiler = await _context.Suppilers.FindAsync(id);
            if (suppiler != null)
            {
                _context.Suppilers.Remove(suppiler);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuppilerExists(int id)
        {
          return (_context.Suppilers?.Any(e => e.SupplierId == id)).GetValueOrDefault();
        }
    }
}
