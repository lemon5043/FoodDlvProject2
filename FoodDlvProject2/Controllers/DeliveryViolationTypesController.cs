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
    public class DeliveryViolationTypesController : Controller
    {
        private readonly AppDbContext _context;

        public DeliveryViolationTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DeliveryViolationTypes
        public async Task<IActionResult> Index()
        {
              return View(await _context.DeliveryViolationTypes.ToListAsync());
        }

        // GET: DeliveryViolationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DeliveryViolationTypes == null)
            {
                return NotFound();
            }

            var deliveryViolationType = await _context.DeliveryViolationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryViolationType == null)
            {
                return NotFound();
            }

            return View(deliveryViolationType);
        }

        // GET: DeliveryViolationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeliveryViolationTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ViolationContent,Content")] DeliveryViolationType deliveryViolationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryViolationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryViolationType);
        }

        // GET: DeliveryViolationTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DeliveryViolationTypes == null)
            {
                return NotFound();
            }

            var deliveryViolationType = await _context.DeliveryViolationTypes.FindAsync(id);
            if (deliveryViolationType == null)
            {
                return NotFound();
            }
            return View(deliveryViolationType);
        }

        // POST: DeliveryViolationTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ViolationContent,Content")] DeliveryViolationType deliveryViolationType)
        {
            if (id != deliveryViolationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryViolationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryViolationTypeExists(deliveryViolationType.Id))
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
            return View(deliveryViolationType);
        }

        // GET: DeliveryViolationTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DeliveryViolationTypes == null)
            {
                return NotFound();
            }

            var deliveryViolationType = await _context.DeliveryViolationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryViolationType == null)
            {
                return NotFound();
            }

            return View(deliveryViolationType);
        }

        // POST: DeliveryViolationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DeliveryViolationTypes == null)
            {
                return Problem("Entity set 'AppDbContext.DeliveryViolationTypes'  is null.");
            }
            var deliveryViolationType = await _context.DeliveryViolationTypes.FindAsync(id);
            if (deliveryViolationType != null)
            {
                _context.DeliveryViolationTypes.Remove(deliveryViolationType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryViolationTypeExists(int id)
        {
          return _context.DeliveryViolationTypes.Any(e => e.Id == id);
        }
    }
}
