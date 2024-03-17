using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiayDep.Models;
using GiayDep.Areas.Admin.InterfacesRepositories;

namespace GiayDep.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepositorycs _repository;

        public CategoryController(ICategoryRepositorycs repository)
        {
            _repository = repository;
        }

        // GET: Admin/Category
        public async Task<IActionResult> Index()
        {
            var Categories = await _repository.GetAllAsync();
            return View(Categories);
        }

        // GET: Admin/Category/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var Category = await _repository.GetByIdAsync(id);
            if (Category == null)
            {
                return NotFound();
            }

            return View(Category);
        }

        // GET: Admin/Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId, CategoryName")] Category _Category)
        {
            
                await _repository.CreateAsync(_Category);
                return RedirectToAction(nameof(Index));
           
        }

        // GET: Admin/Category/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var Category = await _repository.GetByIdAsync(id);
            if (Category == null)
            {
                return NotFound();
            }
            return View(Category);
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category Category)
        {
            if (id != Category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(Category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repository.ExistsAsync(Category.CategoryId).Result)
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
            return View(Category);
        }

        // GET: Admin/Category/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var Category = await _repository.GetByIdAsync(id);
            if (Category == null)
            {
                return NotFound();
            }

            return View(Category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _repository.ExistsAsync(id).Result;
        }
    }
}
