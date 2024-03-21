﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiayDep.Models;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;
using System.Diagnostics;
using System.Text;

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
            var details = _context.InvoiceDetails
                .Include(n => n.ProductVar.ProductItems.Product)
                .Include(n => n.Invoice)
                .ToList();
            return View(details);
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
        [HttpGet]
        // GET: Admin/Invoice/Create
        public IActionResult Create()
        {
            ViewBag.Supplier = new SelectList(_context.Suppliers, "SupplierId", "SupplierName");
            ViewBag.Product = new SelectList(_context.Products, "ProductId", "ProductName");
            ViewBag.ListProduct = _context.ProductVariations
                .Include(n =>n.ProductItems.Product)
                .ToList();
            ViewBag.ListSize = _context.ProductVariations.ToList();
            ViewBag.productItem = new SelectList(_context.ProductVariations, "ProductVarID", "ProductVarID");
            ViewBag.CreateDate = DateTime.Today;
            return View();
        }
        [Authorize(Roles = "Manager")]
        // POST: Admin/Invoice/Create
        // To protect from overposting attacks, enable the specific proper ties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind("InvoiceId,CreateDate,SupplierId")] Invoice model, IEnumerable<InvoiceDetail> lstModel)
        {


            _context.Invoices.Add(model);
            _context.SaveChanges();
          
            ProductVariation product;
            foreach (var item in lstModel)
            {
                product = _context.ProductVariations.SingleOrDefault(n => n.ProductVarId == item.ProductVarId); 
                product.QtyinStock += item.Quanity;
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
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", invoice.SupplierId);
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
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", _Invoice.SupplierId);
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
            ViewBag.Brand = new SelectList(_context.Suppliers.OrderBy(n => n.SupplierName), "SupplierId", "SupplierName");

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
            ViewBag.Brand = new SelectList(_context.Suppliers.OrderBy(n => n.SupplierName), "SupplierId", "SupplierName", model.SupplierId);

            model.CreateDate = DateTime.Now;

            _context.Invoices.Add(model);
            _context.SaveChanges();

            ctpn.InvoiceId = model.InvoiceId;
            Product sp = _context.Products.Single(n => n.ProductId == ctpn.ProductVarId);
            //sp.Quanity += ctpn.Quanity;

            _context.InvoiceDetails.Add(ctpn);
            _context.SaveChanges();

            return RedirectToAction("Index","Products");
        }

     
        public JsonResult GetProductByColor (int id)
        {   
               
            return Json(_context.ProductItems.Where(n=>n.ProductId == id)
                .ToList());    
        }
        public JsonResult GetProductBySize(int id)
        {
            return Json(_context.ProductVariations.Where(n => n.ProductItemsId == id).ToList());
        }
        private bool InvoiceExists(int id)
        {
          return (_context.Invoices?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
        }
    }
}
