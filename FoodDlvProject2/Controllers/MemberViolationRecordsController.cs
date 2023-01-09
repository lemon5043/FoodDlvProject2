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
    public class MemberViolationRecordsController : Controller
    {
        private readonly AppDbContext _context;

        public MemberViolationRecordsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MemberViolationRecords
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MemberViolationRecords.Include(m => m.Member).Include(m => m.Order).Include(m => m.Violation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MemberViolationRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MemberViolationRecords == null)
            {
                return NotFound();
            }

            var memberViolationRecord = await _context.MemberViolationRecords
                .Include(m => m.Member)
                .Include(m => m.Order)
                .Include(m => m.Violation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberViolationRecord == null)
            {
                return NotFound();
            }

            return View(memberViolationRecord);
        }

        // GET: MemberViolationRecords/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["ViolationId"] = new SelectList(_context.MemberViolationTypes, "Id", "Id");
            return View();
        }

        // POST: MemberViolationRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,ViolationId,OrderId,ViolationDate,Id")] MemberViolationRecord memberViolationRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberViolationRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", memberViolationRecord.MemberId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", memberViolationRecord.OrderId);
            ViewData["ViolationId"] = new SelectList(_context.MemberViolationTypes, "Id", "Id", memberViolationRecord.ViolationId);
            return View(memberViolationRecord);
        }

        // GET: MemberViolationRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MemberViolationRecords == null)
            {
                return NotFound();
            }

            var memberViolationRecord = await _context.MemberViolationRecords.FindAsync(id);
            if (memberViolationRecord == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", memberViolationRecord.MemberId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", memberViolationRecord.OrderId);
            ViewData["ViolationId"] = new SelectList(_context.MemberViolationTypes, "Id", "Id", memberViolationRecord.ViolationId);
            return View(memberViolationRecord);
        }

        // POST: MemberViolationRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,ViolationId,OrderId,ViolationDate,Id")] MemberViolationRecord memberViolationRecord)
        {
            if (id != memberViolationRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberViolationRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberViolationRecordExists(memberViolationRecord.Id))
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
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", memberViolationRecord.MemberId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", memberViolationRecord.OrderId);
            ViewData["ViolationId"] = new SelectList(_context.MemberViolationTypes, "Id", "Id", memberViolationRecord.ViolationId);
            return View(memberViolationRecord);
        }

        // GET: MemberViolationRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MemberViolationRecords == null)
            {
                return NotFound();
            }

            var memberViolationRecord = await _context.MemberViolationRecords
                .Include(m => m.Member)
                .Include(m => m.Order)
                .Include(m => m.Violation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberViolationRecord == null)
            {
                return NotFound();
            }

            return View(memberViolationRecord);
        }

        // POST: MemberViolationRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MemberViolationRecords == null)
            {
                return Problem("Entity set 'AppDbContext.MemberViolationRecords'  is null.");
            }
            var memberViolationRecord = await _context.MemberViolationRecords.FindAsync(id);
            if (memberViolationRecord != null)
            {
                _context.MemberViolationRecords.Remove(memberViolationRecord);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberViolationRecordExists(int id)
        {
          return _context.MemberViolationRecords.Any(e => e.Id == id);
        }
    }
}
