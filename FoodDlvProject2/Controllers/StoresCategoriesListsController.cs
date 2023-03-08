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
    public class StoresCategoriesListsController : Controller
    {
        private readonly AppDbContext _context;

        public StoresCategoriesListsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StoresCategoriesLists
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.StoresCategoriesLists.Include(s => s.Category).Include(s => s.Store);
            return View(await appDbContext.ToListAsync());
        }

        // GET: StoresCategoriesLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StoresCategoriesLists == null)
            {
                return NotFound();
            }

            var storesCategoriesList = await _context.StoresCategoriesLists
                .Include(s => s.Category)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storesCategoriesList == null)
            {
                return NotFound();
            }

            return View(storesCategoriesList);
        }

        // GET: StoresCategoriesLists/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.StoreCategories, "Id", "CategoryContent");
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Address");
            return View();
        }

        // POST: StoresCategoriesLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StoreId,CategoryId")] StoresCategoriesList storesCategoriesList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storesCategoriesList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.StoreCategories, "Id", "CategoryContent", storesCategoriesList.CategoryId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Address", storesCategoriesList.StoreId);
            return View(storesCategoriesList);
        }

        // GET: StoresCategoriesLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StoresCategoriesLists == null)
            {
                return NotFound();
            }

            var storesCategoriesList = await _context.StoresCategoriesLists.FindAsync(id);
            if (storesCategoriesList == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.StoreCategories, "Id", "CategoryContent", storesCategoriesList.CategoryId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Address", storesCategoriesList.StoreId);
            return View(storesCategoriesList);
        }

        // POST: StoresCategoriesLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StoreId,CategoryId")] StoresCategoriesList storesCategoriesList)
        {
            if (id != storesCategoriesList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storesCategoriesList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoresCategoriesListExists(storesCategoriesList.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.StoreCategories, "Id", "CategoryContent", storesCategoriesList.CategoryId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Address", storesCategoriesList.StoreId);
            return View(storesCategoriesList);
        }

        // GET: StoresCategoriesLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StoresCategoriesLists == null)
            {
                return NotFound();
            }

            var storesCategoriesList = await _context.StoresCategoriesLists
                .Include(s => s.Category)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storesCategoriesList == null)
            {
                return NotFound();
            }

            return View(storesCategoriesList);
        }

        // POST: StoresCategoriesLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StoresCategoriesLists == null)
            {
                return Problem("Entity set 'AppDbContext.StoresCategoriesLists'  is null.");
            }
            var storesCategoriesList = await _context.StoresCategoriesLists.FindAsync(id);
            if (storesCategoriesList != null)
            {
                _context.StoresCategoriesLists.Remove(storesCategoriesList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoresCategoriesListExists(int id)
        {
          return _context.StoresCategoriesLists.Any(e => e.Id == id);
        }
    }
}
