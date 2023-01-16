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
    public class QasController : Controller
    {
        private readonly AppDbContext _context;

        public QasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Qas
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Qas.Include(q => q.Category);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Qas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Qas == null)
            {
                return NotFound();
            }

            var qa = await _context.Qas
                .Include(q => q.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qa == null)
            {
                return NotFound();
            }

            return View(qa);
        }

        // GET: Qas/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Qacategories, "Id", "Name");
            return View();
        }

        // POST: Qas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,Title,Answer")] Qa qa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Qacategories, "Id", "Name", qa.CategoryId);
            return View(qa);
        }

        // GET: Qas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Qas == null)
            {
                return NotFound();
            }

            var qa = await _context.Qas.FindAsync(id);
            if (qa == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Qacategories, "Id", "Name", qa.CategoryId);
            return View(qa);
        }

        // POST: Qas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,Title,Answer")] Qa qa)
        {
            if (id != qa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QaExists(qa.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Qacategories, "Id", "Name", qa.CategoryId);
            return View(qa);
        }

        // GET: Qas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Qas == null)
            {
                return NotFound();
            }

            var qa = await _context.Qas
                .Include(q => q.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qa == null)
            {
                return NotFound();
            }

            return View(qa);
        }

        // POST: Qas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Qas == null)
            {
                return Problem("Entity set 'AppDbContext.Qas'  is null.");
            }
            var qa = await _context.Qas.FindAsync(id);
            if (qa != null)
            {
                _context.Qas.Remove(qa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QaExists(int id)
        {
          return _context.Qas.Any(e => e.Id == id);
        }
    }
}
