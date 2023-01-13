using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace FoodDlvProject2.Controllers
{
    public class DeliveryViolationRecordsController : Controller
    {
        private readonly AppDbContext _context;

        public DeliveryViolationRecordsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: DeliveryViolationRecords
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DeliveryViolationRecords.Include(d => d.Order).Include(d => d.DeliveryDrivers).Include(v=>v.Violation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DeliveryViolationRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DeliveryDrivers == null)
            {
                return NotFound();
            }

            var DeliveryViolationRecords = _context.DeliveryViolationRecords
                .Include(d => d.DeliveryDrivers)
                .Include(d => d.Order)
                .Include(d=>d.Violation)
                .Where(m => m.DeliveryDriversId == id);
            if (DeliveryViolationRecords == null)
            {
                return NotFound();
            }
			ViewBag.DriverId = id;
			return View(await DeliveryViolationRecords.ToListAsync());
        }
        // GET: DeliveryViolationRecords/Edit/5
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

        // POST: DeliveryViolationRecords/Edit/5
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
        // GET: DeliveryViolationRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DeliveryViolationRecords == null)
            {
                return NotFound();
            }

            var DeliveryViolationRecords = await _context.DeliveryViolationRecords
                .Select(x => new DeliveryViolationRecordVM
                {
                Id=x.Id,
                DriverId = x.DeliveryDriversId,
                DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,           
                Violation = x.Violation.ViolationContent,
                Content= x.Violation.Content,
                ViolationDate = x.ViolationDate,
            }).FirstAsync(x=>x.Id==id);

            if (DeliveryViolationRecords == null)
            {
                return NotFound();
            }

            return View(DeliveryViolationRecords);
        }

        // POST: DeliveryViolationRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DeliveryViolationRecords == null)
            {
                return Problem("Entity set 'AppDbContext.DeliveryViolationRecords'  is null.");
            }
            var DeliveryViolationRecords = await _context.DeliveryViolationRecords.FindAsync(id);
            if (DeliveryViolationRecords != null)
            {
                _context.DeliveryViolationRecords.Remove(DeliveryViolationRecords);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ViolationRecordExists(long id)
        {
            return _context.DeliveryViolationRecords.Any(e => e.OrderId == id);
        }
    }
}
