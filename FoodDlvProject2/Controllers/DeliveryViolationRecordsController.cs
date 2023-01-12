using FoodDlvProject2.EFModels;
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
			ViewBag.DriverId = id;
			return View(await DeliveryRecords.ToListAsync());
        }
        // GET: DeliveryDrivers/Edit/5
        public async Task<IActionResult> Edit(int? OrderId)
        {
            if (OrderId == null || _context.DeliveryViolationRecords == null)
            {
                return NotFound();
            }

            var DeliveryViolationRecords = await _context.DeliveryViolationRecords.FirstOrDefaultAsync(o=>o.OrderId==OrderId);
            if (DeliveryViolationRecords == null)
            {
                return NotFound();
            }
            return View(DeliveryViolationRecords);
        }

        // POST: DeliveryDrivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? OrderId, [Bind("DeliveryDriversId,ViolationId,OrderId,ViolationDate")] DeliveryViolationRecord DeliveryViolationRecord)
        {
            if (OrderId != DeliveryViolationRecord.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(DeliveryViolationRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViolationRecordExists(DeliveryViolationRecord.OrderId))
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

            return View(DeliveryViolationRecord);
        }
        private bool ViolationRecordExists(long id)
        {
            return _context.DeliveryViolationRecords.Any(e => e.OrderId == id);
        }
    }
}
