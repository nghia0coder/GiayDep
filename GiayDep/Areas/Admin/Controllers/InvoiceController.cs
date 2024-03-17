using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiayDep.Models;
using Microsoft.AspNetCore.Authorization;

namespace GiayDep.Areas.Admin.Controllers
{   
    
    [Area("Admin")]
    public class InvoiceController : Controller
    {
        private readonly GiaydepContext _context;

        public InvoiceController(GiaydepContext context)
        {
            _context = context;
        }

        // GET: Admin/Invoice
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index","InvoiceDetails");
        }

        // GET: Admin/Invoice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var Invoice = await _context.Invoices
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (Invoice == null)
            {
                return NotFound();
            }

            return View(Invoice);
        }
        [Authorize(Roles = "Manager")]
        // GET: Admin/Invoice/Create
        public IActionResult Create()
        {
            ViewBag.Brand = _context.Suppilers.ToList();
            ViewBag.ListProduct = _context.Products.ToList();
            ViewBag.CreateDate = DateTime.Today;
            return View();
        }
        [Authorize(Roles = "Manager")]
        // POST: Admin/Invoice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind("InvoiceId,CreateDate,SupplierId")] Invoice model, IEnumerable<InvoiceDetail> lstModel)
        {
            ViewBag.Brand = _context.Suppilers.ToList();
            ViewBag.ListProduct = _context.Products.ToList();
            ViewBag.CreateDate = DateTime.Now;
            model.CreateDate = ViewBag.CreateDate;

            _context.Invoices.Add(model);
            _context.SaveChanges();
            Product product;
            foreach (var item in lstModel)
            {
                product = _context.Products.Single(n => n.ProductId == item.ProductId);
                //product. += item.Quanity;
                item.InvoiceId = model.InvoiceId;
            
            }

            _context.InvoiceDetails.AddRange(lstModel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        // GET: Admin/Invoice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppilers, "SupplierId", "SupplierId", invoice.SupplierId);
            return View(invoice);
        }

        // POST: Admin/Invoice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceId,CreateDate,SupplierId")] Invoice _Invoice)
        {
            if (id != _Invoice.InvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_Invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(_Invoice.InvoiceId))
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
            ViewData["SupplierId"] = new SelectList(_context.Suppilers, "SupplierId", "SupplierId", _Invoice.SupplierId);
            return View(_Invoice);
        }

        // GET: Admin/Invoice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var Invoice = await _context.Invoices
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (Invoice == null)
            {
                return NotFound();
            }

            return View(Invoice);
        }

        // POST: Admin/Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Invoices == null)
            {
                return Problem("Entity set 'GiaydepContext.Invoices'  is null.");
            }
            var Invoice = await _context.Invoices.FindAsync(id);
            if (Invoice != null)
            {
                _context.Invoices.Remove(Invoice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public ActionResult DSSPHetHang()
        {
            //var lstSP = _context.Products.Where(n => n.Quanity <= 5).ToList();
            return View();
        }

        [HttpGet]
        public ActionResult NhapHangDon(int? id)
        {
            ViewBag.Brand = new SelectList(_context.Suppilers.OrderBy(n => n.SupplierName), "SupplierId", "SupplierName");

            if (id == null)
            {
                return NotFound();
            }

            Product sp = _context.Products.SingleOrDefault(n => n.ProductId == id);
            if (sp == null)
            {
                return NotFound();
            }

            return View(sp);
        }

        [HttpPost]
        public ActionResult NhapHangDon(Invoice model, InvoiceDetail ctpn)
        {
            ViewBag.Brand = new SelectList(_context.Suppilers.OrderBy(n => n.SupplierName), "SupplierId", "SupplierName", model.SupplierId);

            model.CreateDate = DateTime.Now;

            _context.Invoices.Add(model);
            _context.SaveChanges();

            ctpn.InvoiceId = model.InvoiceId;
            Product sp = _context.Products.Single(n => n.ProductId == ctpn.ProductId);
            //sp.Quanity += ctpn.Quanity;

            _context.InvoiceDetails.Add(ctpn);
            _context.SaveChanges();

            return RedirectToAction("Index","Products");
        }

        private bool InvoiceExists(int id)
        {
          return (_context.Invoices?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
        }
    }
}
