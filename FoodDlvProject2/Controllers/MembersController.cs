using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.Services;
using FoodDlvProject2.Models.Repositories;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.ViewModels;

namespace FoodDlvProject2.Controllers
{
    public class MembersController : Controller
    {
        private readonly MembersService membersService;

        public MembersController()
        {
            var db = new AppDbContext();
            IMemberRepository repository = new MemberRepository(db);
            this.membersService = new MembersService(repository);
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            var data = membersService.GetMembers().Select(m => m.ToMemberIndexVM());
            return await Task.Run(() => View(data));

        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var data = membersService.GetOnly(id).ToMemberIndexVM();
            return await Task.Run(() => View(data));
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var data = membersService.GetOnly(id).ToMemberEditVM();
            if (data == null) return NotFound();
          
            return View(data);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountStatusId,FirstName,LastName,Phone,Gender,Birthday,Email,Balance,Account,Password,RegistrationTime")] MemberEditVM member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    membersService.Edit(member.ToMemberEditDTO());
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View(member);
                }
                return RedirectToAction(nameof(Index));
            }
           
            return View(membersService);
        }

        // GET: Members/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Members == null)
        //    {
        //        return NotFound();
        //    }

        //    var member = await _context.Members
        //        .Include(m => m.AccountStatus)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (member == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(member);
        //}

        //// POST: Members/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Members == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.Members'  is null.");
        //    }
        //    var member = await _context.Members.FindAsync(id);
        //    if (member != null)
        //    {
        //        _context.Members.Remove(member);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MemberExists(int id)
        //{
        //  return _context.Members.Any(e => e.Id == id);
        //}
    }
}
