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
    public class StoresController : Controller
    {
        private readonly FoodDeliveryContext _context;

        public StoresController(FoodDeliveryContext context)
        {
            _context = context;
        }

        // GET: Stores
        public async Task<IActionResult> Index()
        {
            var foodDeliveryContext = _context.Stores.Include(s => s.StorePrincipal);
            return View(await foodDeliveryContext.ToListAsync());
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var stores = await _context.Stores
                .Include(s => s.StorePrincipal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stores == null)
            {
                return NotFound();
            }

            return View(stores);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            ViewData["StorePrincipalId"] = new SelectList(_context.StorePrincipals, "Id", "Account");
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StorePrincipalId,StoreName,Address,ContactNumber,Opening,Closing")] Stores stores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StorePrincipalId"] = new SelectList(_context.StorePrincipals, "Id", "Account", stores.StorePrincipalId);
            return View(stores);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var stores = await _context.Stores.FindAsync(id);
            if (stores == null)
            {
                return NotFound();
            }
            ViewData["StorePrincipalId"] = new SelectList(_context.StorePrincipals, "Id", "Account", stores.StorePrincipalId);
            return View(stores);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,StorePrincipalId,StoreName,Address,ContactNumber,Opening,Closing")] Stores stores)
        {
            if (id != stores.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoresExists(stores.Id))
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
            ViewData["StorePrincipalId"] = new SelectList(_context.StorePrincipals, "Id", "Account", stores.StorePrincipalId);
            return View(stores);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var stores = await _context.Stores
                .Include(s => s.StorePrincipal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stores == null)
            {
                return NotFound();
            }

            return View(stores);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Stores == null)
            {
                return Problem("Entity set 'FoodDeliveryContext.Stores'  is null.");
            }
            var stores = await _context.Stores.FindAsync(id);
            if (stores != null)
            {
                _context.Stores.Remove(stores);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoresExists(long id)
        {
          return _context.Stores.Any(e => e.Id == id);
        }
    }
}
