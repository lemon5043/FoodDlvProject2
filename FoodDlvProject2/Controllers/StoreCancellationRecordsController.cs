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
    public class StoreCancellationRecordsController : Controller
    {
        private readonly AppDbContext _context;

        public StoreCancellationRecordsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StoreCancellationRecords
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.StoreCancellationRecords.Include(s => s.Cancellation).Include(s => s.Order).Include(s => s.Store);
            return View(await appDbContext.ToListAsync());
        }

        // GET: StoreCancellationRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StoreCancellationRecords == null)
            {
                return NotFound();
            }

            var storeCancellationRecord = await _context.StoreCancellationRecords
                .Include(s => s.Cancellation)
                .Include(s => s.Order)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeCancellationRecord == null)
            {
                return NotFound();
            }

            return View(storeCancellationRecord);
        }

        // GET: StoreCancellationRecords/Create
        public IActionResult Create()
        {
            ViewData["CancellationId"] = new SelectList(_context.StoreCancellationTypes, "Id", "Reason");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress");
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "StoreName");
            return View();
        }

        // POST: StoreCancellationRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CancellationId,OrderId,StoreId,CancellationDate")] StoreCancellationRecord storeCancellationRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeCancellationRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CancellationId"] = new SelectList(_context.StoreCancellationTypes, "Id", "Reason", storeCancellationRecord.CancellationId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", storeCancellationRecord.OrderId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "StoreName", storeCancellationRecord.StoreId);
            return View(storeCancellationRecord);
        }

        // GET: StoreCancellationRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StoreCancellationRecords == null)
            {
                return NotFound();
            }

            var storeCancellationRecord = await _context.StoreCancellationRecords.FindAsync(id);
            if (storeCancellationRecord == null)
            {
                return NotFound();
            }
            ViewData["CancellationId"] = new SelectList(_context.StoreCancellationTypes, "Id", "Reason", storeCancellationRecord.CancellationId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", storeCancellationRecord.OrderId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "StoreName", storeCancellationRecord.StoreId);
            return View(storeCancellationRecord);
        }

        // POST: StoreCancellationRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CancellationId,OrderId,StoreId,CancellationDate")] StoreCancellationRecord storeCancellationRecord)
        {
            if (id != storeCancellationRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeCancellationRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreCancellationRecordExists(storeCancellationRecord.Id))
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
            ViewData["CancellationId"] = new SelectList(_context.StoreCancellationTypes, "Id", "Reason", storeCancellationRecord.CancellationId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "DeliveryAddress", storeCancellationRecord.OrderId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "StoreName", storeCancellationRecord.StoreId);
            return View(storeCancellationRecord);
        }

        // GET: StoreCancellationRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StoreCancellationRecords == null)
            {
                return NotFound();
            }

            var storeCancellationRecord = await _context.StoreCancellationRecords
                .Include(s => s.Cancellation)
                .Include(s => s.Order)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeCancellationRecord == null)
            {
                return NotFound();
            }

            return View(storeCancellationRecord);
        }

        // POST: StoreCancellationRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StoreCancellationRecords == null)
            {
                return Problem("Entity set 'AppDbContext.StoreCancellationRecords'  is null.");
            }
            var storeCancellationRecord = await _context.StoreCancellationRecords.FindAsync(id);
            if (storeCancellationRecord != null)
            {
                _context.StoreCancellationRecords.Remove(storeCancellationRecord);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreCancellationRecordExists(int id)
        {
          return _context.StoreCancellationRecords.Any(e => e.Id == id);
        }
    }
}
