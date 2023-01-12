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
    public class StoreCancellationTypesController : Controller
    {
        private readonly AppDbContext _context;

        public StoreCancellationTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StoreCancellationTypes
        public async Task<IActionResult> Index()
        {
              return View(await _context.StoreCancellationTypes.ToListAsync());
        }

        // GET: StoreCancellationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StoreCancellationTypes == null)
            {
                return NotFound();
            }

            var storeCancellationType = await _context.StoreCancellationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeCancellationType == null)
            {
                return NotFound();
            }

            return View(storeCancellationType);
        }

        // GET: StoreCancellationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoreCancellationTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Reason,Content")] StoreCancellationType storeCancellationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeCancellationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storeCancellationType);
        }

        // GET: StoreCancellationTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StoreCancellationTypes == null)
            {
                return NotFound();
            }

            var storeCancellationType = await _context.StoreCancellationTypes.FindAsync(id);
            if (storeCancellationType == null)
            {
                return NotFound();
            }
            return View(storeCancellationType);
        }

        // POST: StoreCancellationTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Reason,Content")] StoreCancellationType storeCancellationType)
        {
            if (id != storeCancellationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeCancellationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreCancellationTypeExists(storeCancellationType.Id))
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
            return View(storeCancellationType);
        }

        // GET: StoreCancellationTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StoreCancellationTypes == null)
            {
                return NotFound();
            }

            var storeCancellationType = await _context.StoreCancellationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeCancellationType == null)
            {
                return NotFound();
            }

            return View(storeCancellationType);
        }

        // POST: StoreCancellationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StoreCancellationTypes == null)
            {
                return Problem("Entity set 'AppDbContext.StoreCancellationTypes'  is null.");
            }
            var storeCancellationType = await _context.StoreCancellationTypes.FindAsync(id);
            if (storeCancellationType != null)
            {
                _context.StoreCancellationTypes.Remove(storeCancellationType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreCancellationTypeExists(int id)
        {
          return _context.StoreCancellationTypes.Any(e => e.Id == id);
        }
    }
}
