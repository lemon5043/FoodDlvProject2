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
    public class DriverCancellationsController : Controller
    {
        private readonly AppDbContext _context;

        public DriverCancellationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DriverCancellations
        public async Task<IActionResult> Index()
        {
              return View(await _context.DriverCancellations.ToListAsync());
        }

        // GET: DriverCancellations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DriverCancellations == null)
            {
                return NotFound();
            }

            var driverCancellation = await _context.DriverCancellations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driverCancellation == null)
            {
                return NotFound();
            }

            return View(driverCancellation);
        }

        // GET: DriverCancellations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DriverCancellations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Reason,Content")] DriverCancellation driverCancellation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(driverCancellation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(driverCancellation);
        }

        // GET: DriverCancellations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DriverCancellations == null)
            {
                return NotFound();
            }

            var driverCancellation = await _context.DriverCancellations.FindAsync(id);
            if (driverCancellation == null)
            {
                return NotFound();
            }
            return View(driverCancellation);
        }

        // POST: DriverCancellations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Reason,Content")] DriverCancellation driverCancellation)
        {
            if (id != driverCancellation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driverCancellation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverCancellationExists(driverCancellation.Id))
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
            return View(driverCancellation);
        }

        // GET: DriverCancellations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DriverCancellations == null)
            {
                return NotFound();
            }

            var driverCancellation = await _context.DriverCancellations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driverCancellation == null)
            {
                return NotFound();
            }

            return View(driverCancellation);
        }

        // POST: DriverCancellations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DriverCancellations == null)
            {
                return Problem("Entity set 'AppDbContext.DriverCancellations'  is null.");
            }
            var driverCancellation = await _context.DriverCancellations.FindAsync(id);
            if (driverCancellation != null)
            {
                _context.DriverCancellations.Remove(driverCancellation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverCancellationExists(int id)
        {
          return _context.DriverCancellations.Any(e => e.Id == id);
        }
    }
}
