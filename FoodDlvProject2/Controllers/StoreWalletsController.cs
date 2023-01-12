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
    public class StoreWalletsController : Controller
    {
        private readonly AppDbContext _context;

        public StoreWalletsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StoreWallets
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.StoreWallets.Include(s => s.Order).Include(s => s.Store);
            return View(await appDbContext.ToListAsync());
        }

        // GET: StoreWallets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StoreWallets == null)
            {
                return NotFound();
            }

            var storeWallet = await _context.StoreWallets
                .Include(s => s.Order)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeWallet == null)
            {
                return NotFound();
            }

            return View(storeWallet);
        }

        // GET: StoreWallets/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress");
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Address");
            return View();
        }

        // POST: StoreWallets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StoreId,OrderId,Total")] StoreWallet storeWallet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeWallet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", storeWallet.OrderId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Address", storeWallet.StoreId);
            return View(storeWallet);
        }

        // GET: StoreWallets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StoreWallets == null)
            {
                return NotFound();
            }

            var storeWallet = await _context.StoreWallets.FindAsync(id);
            if (storeWallet == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", storeWallet.OrderId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Address", storeWallet.StoreId);
            return View(storeWallet);
        }

        // POST: StoreWallets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StoreId,OrderId,Total")] StoreWallet storeWallet)
        {
            if (id != storeWallet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeWallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreWalletExists(storeWallet.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", storeWallet.OrderId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Address", storeWallet.StoreId);
            return View(storeWallet);
        }

        // GET: StoreWallets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StoreWallets == null)
            {
                return NotFound();
            }

            var storeWallet = await _context.StoreWallets
                .Include(s => s.Order)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeWallet == null)
            {
                return NotFound();
            }

            return View(storeWallet);
        }

        // POST: StoreWallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StoreWallets == null)
            {
                return Problem("Entity set 'AppDbContext.StoreWallets'  is null.");
            }
            var storeWallet = await _context.StoreWallets.FindAsync(id);
            if (storeWallet != null)
            {
                _context.StoreWallets.Remove(storeWallet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreWalletExists(int id)
        {
          return _context.StoreWallets.Any(e => e.Id == id);
        }
    }
}
