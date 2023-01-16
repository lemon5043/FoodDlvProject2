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
    public class CommonRepliesController : Controller
    {
        private readonly AppDbContext _context;

        public CommonRepliesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CommonReplies
        public async Task<IActionResult> Index()
        {
              return View(await _context.CommonReplies.ToListAsync());
        }

        // GET: CommonReplies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CommonReplies == null)
            {
                return NotFound();
            }

            var commonReply = await _context.CommonReplies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commonReply == null)
            {
                return NotFound();
            }

            return View(commonReply);
        }

        // GET: CommonReplies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CommonReplies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreateTime,Answer")] CommonReply commonReply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commonReply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(commonReply);
        }

        // GET: CommonReplies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CommonReplies == null)
            {
                return NotFound();
            }

            var commonReply = await _context.CommonReplies.FindAsync(id);
            if (commonReply == null)
            {
                return NotFound();
            }
            return View(commonReply);
        }

        // POST: CommonReplies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreateTime,Answer")] CommonReply commonReply)
        {
            if (id != commonReply.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commonReply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommonReplyExists(commonReply.Id))
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
            return View(commonReply);
        }

        // GET: CommonReplies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CommonReplies == null)
            {
                return NotFound();
            }

            var commonReply = await _context.CommonReplies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commonReply == null)
            {
                return NotFound();
            }

            return View(commonReply);
        }

        // POST: CommonReplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CommonReplies == null)
            {
                return Problem("Entity set 'AppDbContext.CommonReplies'  is null.");
            }
            var commonReply = await _context.CommonReplies.FindAsync(id);
            if (commonReply != null)
            {
                _context.CommonReplies.Remove(commonReply);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommonReplyExists(int id)
        {
          return _context.CommonReplies.Any(e => e.Id == id);
        }
    }
}
