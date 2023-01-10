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
    public class CustomerServicesController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerServicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CustomerServices
        public async Task<IActionResult> Index()
        {
              return View(await _context.CustomerServices.ToListAsync());
        }

        // GET: CustomerServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomerServices == null)
            {
                return NotFound();
            }

            var customerService = await _context.CustomerServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerService == null)
            {
                return NotFound();
            }

            return View(customerService);
        }

        // GET: CustomerServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Account,Password,Title,Permissions")] CustomerService customerService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerService);
        }

        // GET: CustomerServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerServices == null)
            {
                return NotFound();
            }

            var customerService = await _context.CustomerServices.FindAsync(id);
            if (customerService == null)
            {
                return NotFound();
            }
            return View(customerService);
        }

        // POST: CustomerServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Account,Password,Title,Permissions")] CustomerService customerService)
        {
            if (id != customerService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerServiceExists(customerService.Id))
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
            return View(customerService);
        }

        // GET: CustomerServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomerServices == null)
            {
                return NotFound();
            }

            var customerService = await _context.CustomerServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerService == null)
            {
                return NotFound();
            }

            return View(customerService);
        }

        // POST: CustomerServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustomerServices == null)
            {
                return Problem("Entity set 'AppDbContext.CustomerServices'  is null.");
            }
            var customerService = await _context.CustomerServices.FindAsync(id);
            if (customerService != null)
            {
                _context.CustomerServices.Remove(customerService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerServiceExists(int id)
        {
          return _context.CustomerServices.Any(e => e.Id == id);
        }
    }
}
