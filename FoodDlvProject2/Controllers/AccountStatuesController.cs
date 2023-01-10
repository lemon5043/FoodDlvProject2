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
    public class AccountStatuesController : Controller
    {
        private readonly AppDbContext _context;

        public AccountStatuesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AccountStatues
        public async Task<IActionResult> Index()
        {
              return View(await _context.AccountStatues.ToListAsync());
        }

        // GET: AccountStatues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AccountStatues == null)
            {
                return NotFound();
            }

            var accountStatue = await _context.AccountStatues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountStatue == null)
            {
                return NotFound();
            }

            return View(accountStatue);
        }

        // GET: AccountStatues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountStatues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status")] AccountStatue accountStatue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountStatue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountStatue);
        }

        // GET: AccountStatues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AccountStatues == null)
            {
                return NotFound();
            }

            var accountStatue = await _context.AccountStatues.FindAsync(id);
            if (accountStatue == null)
            {
                return NotFound();
            }
            return View(accountStatue);
        }

        // POST: AccountStatues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status")] AccountStatue accountStatue)
        {
            if (id != accountStatue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountStatue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountStatueExists(accountStatue.Id))
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
            return View(accountStatue);
        }

        // GET: AccountStatues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AccountStatues == null)
            {
                return NotFound();
            }

            var accountStatue = await _context.AccountStatues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountStatue == null)
            {
                return NotFound();
            }

            return View(accountStatue);
        }

        // POST: AccountStatues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AccountStatues == null)
            {
                return Problem("Entity set 'FoodDeliveryContext.AccountStatues'  is null.");
            }
            var accountStatue = await _context.AccountStatues.FindAsync(id);
            if (accountStatue != null)
            {
                _context.AccountStatues.Remove(accountStatue);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountStatueExists(int id)
        {
          return _context.AccountStatues.Any(e => e.Id == id);
        }
    }
}
