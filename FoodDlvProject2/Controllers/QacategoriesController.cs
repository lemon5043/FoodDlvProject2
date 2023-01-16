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
    public class QacategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public QacategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Qacategories
        public async Task<IActionResult> Index()
        {
              return View(await _context.Qacategories.ToListAsync());
        }

        // GET: Qacategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Qacategories == null)
            {
                return NotFound();
            }

            var qacategory = await _context.Qacategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qacategory == null)
            {
                return NotFound();
            }

            return View(qacategory);
        }

        // GET: Qacategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Qacategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Displayorder")] Qacategory qacategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qacategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(qacategory);
        }

        // GET: Qacategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Qacategories == null)
            {
                return NotFound();
            }

            var qacategory = await _context.Qacategories.FindAsync(id);
            if (qacategory == null)
            {
                return NotFound();
            }
            return View(qacategory);
        }

        // POST: Qacategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Displayorder")] Qacategory qacategory)
        {
            if (id != qacategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qacategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QacategoryExists(qacategory.Id))
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
            return View(qacategory);
        }

        // GET: Qacategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Qacategories == null)
            {
                return NotFound();
            }

            var qacategory = await _context.Qacategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qacategory == null)
            {
                return NotFound();
            }

            return View(qacategory);
        }

        // POST: Qacategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Qacategories == null)
            {
                return Problem("Entity set 'AppDbContext.Qacategories'  is null.");
            }
            var qacategory = await _context.Qacategories.FindAsync(id);
            if (qacategory != null)
            {
                _context.Qacategories.Remove(qacategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QacategoryExists(int id)
        {
          return _context.Qacategories.Any(e => e.Id == id);
        }
    }
}
