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
            var appDbContext = _context.DeliveryViolationRecords
                .Select(x => new DeliveryViolationRecordsIndexVM
                {
                    Id = x.Id,
                    DriverId = x.DeliveryDriversId,
                    DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                    OrderId = x.OrderId,
                    ViolationContent = x.Violation.ViolationContent,
                    ViolationDate = x.ViolationDate,
                });
            return View(await appDbContext.ToListAsync());
        }

        // GET: DeliveryViolationRecords/PersonalDetails/5
        public async Task<IActionResult> PersonalDetails(int? id)
        {
            if (id == null || _context.DeliveryViolationRecords == null)
            {
                return NotFound();
            }

            var DeliveryViolationRecords = _context.DeliveryViolationRecords
                .Where(m => m.DeliveryDriversId == id)
                .Select(x => new DeliveryViolationRecordPersonalDetailsVM
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                    ViolationContent = x.Violation.ViolationContent,
                    Content = x.Violation.ViolationContent,
                    ViolationDate = x.ViolationDate,
                });
            if (DeliveryViolationRecords == null)
            {
                return NotFound();
            }
            ViewBag.DriverId = id;
            ViewBag.DriverName = DeliveryViolationRecords.Select(x => x.DriverName).FirstOrDefault();

            return View(await DeliveryViolationRecords.ToListAsync());
        }
        // GET: DeliveryViolationRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DeliveryViolationRecords == null)
            {
                return NotFound();
            }

            var DeliveryViolationRecords = _context.DeliveryViolationRecords
                .Where(m => m.Id == id)
                .Select(x => new DeliveryViolationRecordDetailsVM
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                    ViolationContent = x.Violation.ViolationContent,
                    Content = x.Violation.ViolationContent,
                    ViolationDate = x.ViolationDate,
                }).FirstOrDefaultAsync();

            if (DeliveryViolationRecords == null)
            {
                return NotFound();
            }

            ViewBag.DriverId = id;

            return View(await DeliveryViolationRecords);
        }
        // GET: DeliveryViolationRecords/Edit/5
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null || _context.DeliveryViolationRecords == null)
            {
                return NotFound();
            }

            var DeliveryViolationRecords = await _context.DeliveryViolationRecords
                .Where(o => o.Id == Id)
                .Select(x => new DeliveryViolationRecordEditVM
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    DriverId = x.DeliveryDriversId,
                    DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                    ViolationDate = x.ViolationDate,
                    ViolationId = x.ViolationId
                })
                .FirstOrDefaultAsync();
            ViewData["ViolationId"] = new SelectList(_context.DeliveryViolationTypes, "Id", "ViolationContent", DeliveryViolationRecords.ViolationId);
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
        public async Task<IActionResult> Edit(int? id, [Bind("Id,ViolationId,ViolationDate")] DeliveryViolationRecordEditVM DeliveryViolationRecord)
        {
            if (id != DeliveryViolationRecord.Id)
            {
                return NotFound();
            }
            ModelState.Remove("DriverName");
            if (ModelState.IsValid)
            {
                try
                {
                    var EFModel = DeliveryViolationRecord.ToEFModels();
                    _context.Attach(EFModel);
                    string[] updateModel = { "ViolationId", "ViolationDate" };

                    foreach (var property in updateModel)
                    {
                        _context.Entry(EFModel).Property(property).IsModified = true;
                    }

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
            var data = await _context.DeliveryViolationRecords
                 .Where(o => o.Id == id)
                 .Select(x => new DeliveryViolationRecordEditVM
                 {
                     Id = x.Id,
                     OrderId = x.OrderId,
                     DriverId = x.DeliveryDriversId,
                     DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                     ViolationDate = DeliveryViolationRecord.ViolationDate,
                     ViolationId = DeliveryViolationRecord.ViolationId
                 })
                 .FirstOrDefaultAsync();
            ViewData["ViolationId"] = new SelectList(_context.DeliveryViolationTypes, "Id", "ViolationContent", DeliveryViolationRecord.ViolationId);
            return View(data);
        }
        // GET: DeliveryViolationRecords/Delete/5

        // GET: Pays/Create
        public IActionResult Create()
        {
            ViewData["ViolationId"] = new SelectList(_context.DeliveryViolationTypes, "Id", "ViolationContent");
            return View();
        }

        // POST: Pays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,,DriverName,ViolationId,ViolationDate")] DeliveryViolationRecordCreateVM DeliveryViolationRecord)
        {
            ModelState.Remove("DriverName");
            if (ModelState.IsValid)
            {
                try
                {
                    var EFModel = DeliveryViolationRecord.ToEFModels();
                    _context.Attach(EFModel);
                    string[] updateModel = { "DeliveryDriversId", "OrderId", "ViolationId", "ViolationDate" };

                    foreach (var property in updateModel)
                    {
                        _context.Entry(EFModel).Property(property).IsModified = true;
                    }
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

            ViewData["ViolationId"] = new SelectList(_context.DeliveryViolationTypes, "Id", "ViolationContent", DeliveryViolationRecord.ViolationId);
            return View(DeliveryViolationRecord);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DeliveryViolationRecords == null)
            {
                return NotFound();
            }

            var DeliveryViolationRecords = await _context.DeliveryViolationRecords
                .Select(x => new DeliveryViolationRecordVM
                {
                    Id = x.Id,
                    DriverId = x.DeliveryDriversId,
                    DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                    Violation = x.Violation.ViolationContent,
                    Content = x.Violation.Content,
                    ViolationDate = x.ViolationDate,
                }).FirstAsync(x => x.Id == id);

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
