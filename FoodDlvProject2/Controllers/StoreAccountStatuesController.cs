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
    public class StoreAccountStatuesController : Controller
    {
        private readonly FoodDeliveryContext _context;

        public StoreAccountStatuesController(FoodDeliveryContext context)
        {
            _context = context;
        }

        // GET: StoreAccountStatues
        public async Task<IActionResult> Index()
        {
              return View(await _context.StoreAccountStatues.ToListAsync());
        }

        // GET: StoreAccountStatues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StoreAccountStatues == null)
            {
                return NotFound();
            }

            var storeAccountStatues = await _context.StoreAccountStatues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeAccountStatues == null)
            {
                return NotFound();
            }

            return View(storeAccountStatues);
        }

        // GET: StoreAccountStatues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StoreAccountStatues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status")] StoreAccountStatues storeAccountStatues)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeAccountStatues);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storeAccountStatues);
        }

        // GET: StoreAccountStatues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StoreAccountStatues == null)
            {
                return NotFound();
            }

            var storeAccountStatues = await _context.StoreAccountStatues.FindAsync(id);
            if (storeAccountStatues == null)
            {
                return NotFound();
            }
            return View(storeAccountStatues);
        }

        // POST: StoreAccountStatues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status")] StoreAccountStatues storeAccountStatues)
        {
            if (id != storeAccountStatues.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeAccountStatues);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreAccountStatuesExists(storeAccountStatues.Id))
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
            return View(storeAccountStatues);
        }

        // GET: StoreAccountStatues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StoreAccountStatues == null)
            {
                return NotFound();
            }

            var storeAccountStatues = await _context.StoreAccountStatues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeAccountStatues == null)
            {
                return NotFound();
            }

            return View(storeAccountStatues);
        }

        // POST: StoreAccountStatues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StoreAccountStatues == null)
            {
                return Problem("Entity set 'FoodDeliveryContext.StoreAccountStatues'  is null.");
            }
            var storeAccountStatues = await _context.StoreAccountStatues.FindAsync(id);
            if (storeAccountStatues != null)
            {
                _context.StoreAccountStatues.Remove(storeAccountStatues);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreAccountStatuesExists(int id)
        {
          return _context.StoreAccountStatues.Any(e => e.Id == id);
        }
    }
}
