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
    public class StorePrincipalsController : Controller
    {
        private readonly FoodDeliveryContext _context;

        public StorePrincipalsController(FoodDeliveryContext context)
        {
            _context = context;
        }

        // GET: StorePrincipals
        public async Task<IActionResult> Index()
        {
            var foodDeliveryContext = _context.StorePrincipals.Include(s => s.AccountStatus);
            return View(await foodDeliveryContext.ToListAsync());
        }

        // GET: StorePrincipals/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.StorePrincipals == null)
            {
                return NotFound();
            }

            var storePrincipals = await _context.StorePrincipals
                .Include(s => s.AccountStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storePrincipals == null)
            {
                return NotFound();
            }

            return View(storePrincipals);
        }

        // GET: StorePrincipals/Create
        public IActionResult Create()
        {
            ViewData["AccountStatusId"] = new SelectList(_context.StoreAccountStatues, "Id", "Status");
            return View();
        }

        // POST: StorePrincipals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountStatusId,Name,Phone,Gender,IdentityCode,Birthday,Email,Address,Account,Password,RegistrationTime")] StorePrincipals storePrincipals)
        {
            if (ModelState.IsValid)
            {
                storePrincipals.RegistrationTime = DateTime.Now;
                _context.Add(storePrincipals);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountStatusId"] = new SelectList(_context.StoreAccountStatues, "Id", "Status", storePrincipals.AccountStatusId);
            return View(storePrincipals);
        }

        // GET: StorePrincipals/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.StorePrincipals == null)
            {
                return NotFound();
            }

            var storePrincipals = await _context.StorePrincipals.FindAsync(id);
            if (storePrincipals == null)
            {
                return NotFound();
            }
            ViewData["AccountStatusId"] = new SelectList(_context.StoreAccountStatues, "Id", "Status", storePrincipals.AccountStatusId);
            return View(storePrincipals);
        }

        // POST: StorePrincipals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,AccountStatusId,Name,Phone,Gender,IdentityCode,Birthday,Email,Address,Account,Password,RegistrationTime")] StorePrincipals storePrincipals)
        {
            if (id != storePrincipals.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storePrincipals);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StorePrincipalsExists(storePrincipals.Id))
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
            ViewData["AccountStatusId"] = new SelectList(_context.StoreAccountStatues, "Id", "Status", storePrincipals.AccountStatusId);
            return View(storePrincipals);
        }

        // GET: StorePrincipals/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.StorePrincipals == null)
            {
                return NotFound();
            }

            var storePrincipals = await _context.StorePrincipals
                .Include(s => s.AccountStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storePrincipals == null)
            {
                return NotFound();
            }

            return View(storePrincipals);
        }

        // POST: StorePrincipals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.StorePrincipals == null)
            {
                return Problem("Entity set 'FoodDeliveryContext.StorePrincipals'  is null.");
            }
            var storePrincipals = await _context.StorePrincipals.FindAsync(id);
            if (storePrincipals != null)
            {
                _context.StorePrincipals.Remove(storePrincipals);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StorePrincipalsExists(long id)
        {
          return _context.StorePrincipals.Any(e => e.Id == id);
        }
    }
}
