using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDlvProject2.EFModels;

namespace FoodDlvProject2.Controllers
{
    public class StoreCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public StoreCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StoreCategories
        public async Task<IActionResult> Index()
        {
              return View(await _context.StoreCategories.ToListAsync());
        }

        // GET: StoreCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StoreCategories == null)
            {
                return NotFound();
            }

            var storeCategory = await _context.StoreCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeCategory == null)
            {
                return NotFound();
            }

            return View(storeCategory);
        }

        // GET: StoreCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoreCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,CategoryContent")] StoreCategory storeCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storeCategory);
        }

        // GET: StoreCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StoreCategories == null)
            {
                return NotFound();
            }

            var storeCategory = await _context.StoreCategories.FindAsync(id);
            if (storeCategory == null)
            {
                return NotFound();
            }
            return View(storeCategory);
        }

        // POST: StoreCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryName,CategoryContent")] StoreCategory storeCategory)
        {
            if (id != storeCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreCategoryExists(storeCategory.Id))
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
            return View(storeCategory);
        }

        // GET: StoreCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StoreCategories == null)
            {
                return NotFound();
            }

            var storeCategory = await _context.StoreCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeCategory == null)
            {
                return NotFound();
            }

            return View(storeCategory);
        }

        // POST: StoreCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StoreCategories == null)
            {
                return Problem("Entity set 'AppDbContext.StoreCategories'  is null.");
            }
            var storeCategory = await _context.StoreCategories.FindAsync(id);
            if (storeCategory != null)
            {
                _context.StoreCategories.Remove(storeCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreCategoryExists(int id)
        {
          return _context.StoreCategories.Any(e => e.Id == id);
        }
    }
}
