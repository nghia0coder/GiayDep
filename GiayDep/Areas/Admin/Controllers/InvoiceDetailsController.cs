using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiayDep.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace PriceyDep.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InvoiceDetailsController : Controller
    {
        private readonly GiaydepContext _context;

        public InvoiceDetailsController(GiaydepContext context)
        {
            _context = context;
        }

        // GET: Admin/InvoiceDetails
        public async Task<IActionResult> Index()
        {
            var PriceydepContext = _context.InvoiceDetails.Include(c => c.InvoiceId);
            return View(await PriceydepContext.ToListAsync());
        }

        // GET: Admin/InvoiceDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var InvoiceDetail = _context.InvoiceDetails
                .Include(c => c.InvoiceId)
                .ToList();
              
            if (InvoiceDetail == null)
            {
                return NotFound();
            }

            return View(InvoiceDetail);
        }

        // GET: Admin/InvoiceDetails/Create
        public IActionResult Create()
        {
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: Admin/InvoiceDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InProductId,InvoiceId,Quanity,Price")] InvoiceDetail _InvoiceDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(_InvoiceDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId", _InvoiceDetail.InvoiceId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", _InvoiceDetail.ProductId);
            return View(_InvoiceDetail);
        }

        // GET: Admin/InvoiceDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }
          
            var InvoiceDetail = await _context.InvoiceDetails.FindAsync(id);
            if (InvoiceDetail == null)
            {
                return NotFound();
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId", InvoiceDetail.InvoiceId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", InvoiceDetail.ProductId);
            return View(InvoiceDetail);
        }

        // POST: Admin/InvoiceDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdchitietPn,ProductId,InvoiceId,Quanity,Price")] InvoiceDetail InvoiceDetail)
        {
          

            if (ModelState.IsValid)
            {
           
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "InvoiceId", "InvoiceId", InvoiceDetail.InvoiceId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", InvoiceDetail.ProductId);
            return View(InvoiceDetail);
        }

        // GET: Admin/InvoiceDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }
            var InvoiceDetail = _context.InvoiceDetails
                .Include(c => c.InvoiceId)
                .Include(c => c.ProductId);
         
            if (InvoiceDetail == null)
            {
                return NotFound();
            }
            return View(InvoiceDetail);
        }

        // POST: Admin/InvoiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InvoiceDetails == null)
            {
                return Problem("Entity set 'PriceydepContext.InvoiceDetails'  is null.");
            }
            var InvoiceDetail = await _context.InvoiceDetails.FindAsync(id);
            if (InvoiceDetail != null)
            {
                _context.InvoiceDetails.Remove(InvoiceDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceDetailExists(int id)
        {
          return (_context.InvoiceDetails?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
        }
    }
}
