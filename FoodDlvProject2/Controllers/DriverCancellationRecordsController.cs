using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace FoodDlvProject2.Controllers
{
    public class DriverCancellationRecordsController : Controller
    {
        private readonly AppDbContext _context;

        public DriverCancellationRecordsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: DeliveryDrivers
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DriverCancellationRecords
                .Select(x => new DriverCancellationRecordsIndexVM
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    DriverId = x.DeliveryDriversId,
                    DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                    Reason = x.Cancellation.Reason,
                    CancellationDate = x.CancellationDate,
                });
            return View(await appDbContext.ToListAsync());
        }

        // GET: DeliveryDrivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DeliveryDrivers == null)
            {
                return NotFound();
            }

            var DeliveryRecords = _context.DriverCancellationRecords
                .Where(m => m.DeliveryDriversId == id)
                .Select(x => new DriverCancellationRecordsDetailsVM
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    DriverId = x.DeliveryDriversId,
                    DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                    Reason = x.Cancellation.Reason,
                    Context = x.Cancellation.Content,
                    CancellationDate = x.CancellationDate,
                });

            if (DeliveryRecords == null)
            {
                return NotFound();
            }
            ViewBag.DriverId = id;
            ViewBag.DriverName = DeliveryRecords.Select(x => x.DriverName).FirstOrDefault();
            return View(await DeliveryRecords.ToListAsync());
        }
        // GET: DeliveryDrivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DriverCancellationRecords == null)
            {
                return NotFound();
            }

            var DriverCancellationRecords = await _context.DriverCancellationRecords
                .Where(i => i.Id == id)
                .Select(x => new DriverCancellationRecordsEditVM
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    DriverId = x.DeliveryDriversId,
                    DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                    CancellationId = x.CancellationId,
                    CancellationDate = x.CancellationDate,
                })
                .FirstOrDefaultAsync();
            if (DriverCancellationRecords == null)
            {
                return NotFound();
            }
            ViewData["CancellationId"] = new SelectList(_context.DriverCancellations, "Id", "Reason", DriverCancellationRecords.CancellationId);
            return View(DriverCancellationRecords);
        }

        // POST: DeliveryDrivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CancellationId,CancellationDate")] DriverCancellationRecordsEditVM DriverCancellationRecords)
        {
            if (id != DriverCancellationRecords.Id)
            {
                return NotFound();
            }
            ModelState.Remove("DriverName");
            if (ModelState.IsValid)
            {
                try
                {
                    var EFModel = DriverCancellationRecords.ToEFModels();
                    _context.Attach(EFModel);
                    string[] updateModel = { "CancellationId", "CancellationDate" };

                    foreach (var property in updateModel)
                    {
                        _context.Entry(EFModel).Property(property).IsModified = true;
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CancellationRecordsExists(DriverCancellationRecords.Id))
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
            var data = await _context.DriverCancellationRecords
                .Where(i => i.Id == id)
                .Select(x => new DriverCancellationRecordsEditVM
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    DriverId = x.DeliveryDriversId,
                    DriverName = x.DeliveryDrivers.LastName + x.DeliveryDrivers.FirstName,
                    CancellationId = DriverCancellationRecords.CancellationId,
                    CancellationDate = DriverCancellationRecords.CancellationDate,
                })
                .FirstOrDefaultAsync();
            ViewData["CancellationId"] = new SelectList(_context.DriverCancellations, "Id", "Reason", DriverCancellationRecords.CancellationId);
            return View(data);
        }
        private bool CancellationRecordsExists(int id)
        {
            return _context.DriverCancellationRecords.Any(e => e.Id == id);
        }
    }
}
