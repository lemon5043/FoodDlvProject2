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
    public class AppealRecordsController : Controller
    {
        private readonly AppDbContext _context;

        public AppealRecordsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AppealRecords
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AppealRecords.Include(a => a.Complaint).Include(a => a.Order).Include(a => a.Status);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AppealRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AppealRecords == null)
            {
                return NotFound();
            }

            var appealRecord = await _context.AppealRecords
                .Include(a => a.Complaint)
                .Include(a => a.Order)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appealRecord == null)
            {
                return NotFound();
            }

            return View(appealRecord);
        }

        // GET: AppealRecords/Create
        public IActionResult Create()
        {
            ViewData["ComplaintId"] = new SelectList(_context.ComplaintTypes, "Id", "ComplaintType1");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryDriversId");
            ViewData["StatusId"] = new SelectList(_context.ComplaintStatuses, "Id", "Status");
            return View();
        }

        // POST: AppealRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderId,CreateTime,ComplaintId,Content,StatusId")] AppealRecord appealRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appealRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComplaintId"] = new SelectList(_context.ComplaintTypes, "Id", "ComplaintType1", appealRecord.ComplaintId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", appealRecord.OrderId);
            ViewData["StatusId"] = new SelectList(_context.ComplaintStatuses, "Id", "Status", appealRecord.StatusId);
            return View(appealRecord);
        }

        // GET: AppealRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AppealRecords == null)
            {
                return NotFound();
            }

            var appealRecord = await _context.AppealRecords.FindAsync(id);
            if (appealRecord == null)
            {
                return NotFound();
            }
            ViewData["ComplaintId"] = new SelectList(_context.ComplaintTypes, "Id", "ComplaintType1", appealRecord.ComplaintId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", appealRecord.OrderId);
            ViewData["StatusId"] = new SelectList(_context.ComplaintStatuses, "Id", "Status", appealRecord.StatusId);
            return View(appealRecord);
        }

        // POST: AppealRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,CreateTime,ComplaintId,Content,StatusId")] AppealRecord appealRecord)
        {
            if (id != appealRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appealRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppealRecordExists(appealRecord.Id))
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
            ViewData["ComplaintId"] = new SelectList(_context.ComplaintTypes, "Id", "ComplaintType1", appealRecord.ComplaintId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", appealRecord.OrderId);
            ViewData["StatusId"] = new SelectList(_context.ComplaintStatuses, "Id", "Status", appealRecord.StatusId);
            return View(appealRecord);
        }

        // GET: AppealRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AppealRecords == null)
            {
                return NotFound();
            }

            var appealRecord = await _context.AppealRecords
                .Include(a => a.Complaint)
                .Include(a => a.Order)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appealRecord == null)
            {
                return NotFound();
            }

            return View(appealRecord);
        }

        // POST: AppealRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AppealRecords == null)
            {
                return Problem("Entity set 'AppDbContext.AppealRecords'  is null.");
            }
            var appealRecord = await _context.AppealRecords.FindAsync(id);
            if (appealRecord != null)
            {
                _context.AppealRecords.Remove(appealRecord);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppealRecordExists(int id)
        {
          return _context.AppealRecords.Any(e => e.Id == id);
        }
    }
}
