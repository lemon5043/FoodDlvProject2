using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDlvProject2.Models.EFModels;

namespace FoodDlvProject2.Controllers
{
    public class DeliveryDriversController : Controller
    {
        private readonly AppDbContext _context;

        public DeliveryDriversController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DeliveryDrivers
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DeliveryDrivers.Include(d => d.AccountStatus).Include(d => d.WorkStatuse);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DeliveryDrivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DeliveryDrivers == null)
            {
                return NotFound();
            }

            var deliveryDriver = await _context.DeliveryDrivers
                .Include(d => d.AccountStatus)
                .Include(d => d.WorkStatuse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryDriver == null)
            {
                return NotFound();
            }

            return View(deliveryDriver);
        }

        // GET: DeliveryDrivers/Create
        public IActionResult Create()
        {
            ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Id");
            ViewData["WorkStatuseId"] = new SelectList(_context.DeliveryDriverWorkStatuses, "Id", "Id");
            return View();
        }

        // POST: DeliveryDrivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountStatusId,WorkStatuseId,FirstName,LastName,Phone,Gender,BankAccount,Idcard,RegistrationTime,VehicleRegistration,Birthday,Email,Account,Password,DriverLicense,Longitude,Latitude")] DeliveryDriver deliveryDriver)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryDriver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Id", deliveryDriver.AccountStatusId);
            ViewData["WorkStatuseId"] = new SelectList(_context.DeliveryDriverWorkStatuses, "Id", "Id", deliveryDriver.WorkStatuseId);
            return View(deliveryDriver);
        }

        // GET: DeliveryDrivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DeliveryDrivers == null)
            {
                return NotFound();
            }

            var deliveryDriver = await _context.DeliveryDrivers.FindAsync(id);
            if (deliveryDriver == null)
            {
                return NotFound();
            }
            ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Status", deliveryDriver.AccountStatusId);
            ViewData["WorkStatuseId"] = new SelectList(_context.DeliveryDriverWorkStatuses, "Id", "Status", deliveryDriver.WorkStatuseId);
            return View(deliveryDriver);
        }

        // POST: DeliveryDrivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountStatusId,WorkStatuseId,FirstName,LastName,Phone,Gender,BankAccount,Idcard,RegistrationTime,VehicleRegistration,Birthday,Email,Account,Password,DriverLicense,Longitude,Latitude")] DeliveryDriver deliveryDriver)
        {
            if (id != deliveryDriver.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryDriver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryDriverExists(deliveryDriver.Id))
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
            ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Status", deliveryDriver.AccountStatusId);
            ViewData["WorkStatuseId"] = new SelectList(_context.DeliveryDriverWorkStatuses, "Id", "Status", deliveryDriver.WorkStatuseId);
            return View(deliveryDriver);
        }

        // GET: DeliveryDrivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DeliveryDrivers == null)
            {
                return NotFound();
            }

            var deliveryDriver = await _context.DeliveryDrivers
                .Include(d => d.AccountStatus)
                .Include(d => d.WorkStatuse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryDriver == null)
            {
                return NotFound();
            }

            return View(deliveryDriver);
        }

        // POST: DeliveryDrivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DeliveryDrivers == null)
            {
                return Problem("Entity set 'AppDbContext.DeliveryDrivers'  is null.");
            }
            var deliveryDriver = await _context.DeliveryDrivers.FindAsync(id);
            if (deliveryDriver != null)
            {
                _context.DeliveryDrivers.Remove(deliveryDriver);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryDriverExists(int id)
        {
          return _context.DeliveryDrivers.Any(e => e.Id == id);
        }
    }
}
