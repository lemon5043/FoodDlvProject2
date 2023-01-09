using FoodDlvProject2.Models.EFModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FoodDlvProject2.Controllers
{
    public class DeliveryViolationRecordsController : Controller
    {
        private readonly AppDbContext _context;

        public DeliveryViolationRecordsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: DeliveryDrivers
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DeliveryViolationRecords.Include(d => d.Order).Include(d => d.DeliveryDrivers).Include(v=>v.Violation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DeliveryDrivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DeliveryDrivers == null)
            {
                return NotFound();
            }

            var DeliveryRecords = _context.DeliveryViolationRecords
                .Include(d => d.DeliveryDrivers)
                .Include(d => d.Order)
                .Include(d=>d.Violation)
                .Where(m => m.DeliveryDriversId == id);
            if (DeliveryRecords == null)
            {
                return NotFound();
            }

            return View(await DeliveryRecords.ToListAsync());
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
            ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Id", deliveryDriver.AccountStatusId);
            ViewData["WorkStatuseId"] = new SelectList(_context.DeliveryDriverWorkStatuses, "Id", "Id", deliveryDriver.WorkStatuseId);
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
            ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Id", deliveryDriver.AccountStatusId);
            ViewData["WorkStatuseId"] = new SelectList(_context.DeliveryDriverWorkStatuses, "Id", "Id", deliveryDriver.WorkStatuseId);
            return View(deliveryDriver);
        }
        private bool DeliveryDriverExists(int id)
        {
            return _context.DeliveryDrivers.Any(e => e.Id == id);
        }
    }
}
