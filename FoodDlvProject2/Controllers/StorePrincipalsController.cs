using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDlvProject2.EFModels;
using System.Security.Principal;

namespace FoodDlvProject2.Controllers
{
    public class StorePrincipalsController : Controller
    {
        private readonly AppDbContext _context;

        public StorePrincipalsController(AppDbContext context)
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StorePrincipals == null)
            {
                return NotFound();
            }

            var storePrincipal = await _context.StorePrincipals
                .Include(s => s.AccountStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storePrincipal == null)
            {
                return NotFound();
            }

            return View(storePrincipal);
        }

        // GET: StorePrincipals/Create
        public IActionResult Create()
        {
            ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Status");
            return View();
        }

        // POST: StorePrincipals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountStatusId,FirstName,LastName,Phone,Gender,Birthday,Email,Account,Password,RegistrationTime")] StorePrincipal storePrincipal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storePrincipal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Status", storePrincipal.AccountStatusId);
            return View(storePrincipal);
        }

        // GET: StorePrincipals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StorePrincipals == null)
            {
                return NotFound();
            }

            var storePrincipal = await _context.StorePrincipals.FindAsync(id);
            if (storePrincipal == null)
            {
                return NotFound();
            }
            ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Status", storePrincipal.AccountStatusId);
            return View(storePrincipal);
        }

        // POST: StorePrincipals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountStatusId,FirstName,LastName,Phone,Gender,Birthday,Email,Account,Password,RegistrationTime")] StorePrincipal storePrincipal)
        {
            if (id != storePrincipal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storePrincipal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StorePrincipalExists(storePrincipal.Id))
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
            ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Status", storePrincipal.AccountStatusId);
            return View(storePrincipal);
        }

        // GET: StorePrincipals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StorePrincipals == null)
            {
                return NotFound();
            }

            var storePrincipal = await _context.StorePrincipals
                .Include(s => s.AccountStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storePrincipal == null)
            {
                return NotFound();
            }

            return View(storePrincipal);
        }

        // POST: StorePrincipals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StorePrincipals == null)
            {
                return Problem("Entity set 'FoodDeliveryContext.StorePrincipals'  is null.");
            }
            var storePrincipal = await _context.StorePrincipals.FindAsync(id);
            if (storePrincipal != null)
            {
                _context.StorePrincipals.Remove(storePrincipal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StorePrincipalExists(int id)
        {
          return _context.StorePrincipals.Any(e => e.Id == id);
        }
    }
}
