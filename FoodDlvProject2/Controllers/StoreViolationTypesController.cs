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
    public class StoreViolationTypesController : Controller
    {
        private readonly AppDbContext _context;

        public StoreViolationTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StoreViolationTypes
        public async Task<IActionResult> Index()
        {
              return View(await _context.StoreViolationTypes.ToListAsync());
        }

        // GET: StoreViolationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StoreViolationTypes == null)
            {
                return NotFound();
            }

            var storeViolationType = await _context.StoreViolationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeViolationType == null)
            {
                return NotFound();
            }

            return View(storeViolationType);
        }

        // GET: StoreViolationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoreViolationTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ViolationContent,Content")] StoreViolationType storeViolationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeViolationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storeViolationType);
        }

        // GET: StoreViolationTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StoreViolationTypes == null)
            {
                return NotFound();
            }

            var storeViolationType = await _context.StoreViolationTypes.FindAsync(id);
            if (storeViolationType == null)
            {
                return NotFound();
            }
            return View(storeViolationType);
        }

        // POST: StoreViolationTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ViolationContent,Content")] StoreViolationType storeViolationType)
        {
            if (id != storeViolationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeViolationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreViolationTypeExists(storeViolationType.Id))
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
            return View(storeViolationType);
        }

        // GET: StoreViolationTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StoreViolationTypes == null)
            {
                return NotFound();
            }

            var storeViolationType = await _context.StoreViolationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeViolationType == null)
            {
                return NotFound();
            }

            return View(storeViolationType);
        }

        // POST: StoreViolationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StoreViolationTypes == null)
            {
                return Problem("Entity set 'AppDbContext.StoreViolationTypes'  is null.");
            }
            var storeViolationType = await _context.StoreViolationTypes.FindAsync(id);
            if (storeViolationType != null)
            {
                _context.StoreViolationTypes.Remove(storeViolationType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreViolationTypeExists(int id)
        {
          return _context.StoreViolationTypes.Any(e => e.Id == id);
        }
    }
}
