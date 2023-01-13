using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.ViewModels;
using FoodDlvProject2.Models.Services;
using NuGet.Protocol.Core.Types;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Security.Principal;

namespace FoodDlvProject2.Controllers
{
    public class StaffsController : Controller
    {

        //精靈做的
        private readonly AppDbContext _context;

        //自定義
        private StaffService service;
        private IStaffRepository repo;

        public StaffsController(AppDbContext context)
        {
            _context = context;
            repo = new StaffRepository();
            service = new StaffService(repo);
        }

        // GET: Staffs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Staffs.ToListAsync());
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Account,Password,Title,Permissions,RegistrationTime")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Account,Password,Title,Permissions,RegistrationTime")] Staff staff)
        {
            if (id != staff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.Id))
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
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Staffs == null)
            {
                return Problem("Entity set 'AppDbContext.Staffs'  is null.");
            }
            var staff = await _context.Staffs.FindAsync(id);
            if (staff != null)
            {
                _context.Staffs.Remove(staff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
            return _context.Staffs.Any(e => e.Id == id);
        }


        //自定義
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(StaffLoginVM model)
        {
            // var service = new MemberService(repository);
            (bool IsSuccess, string ErrorMessage) response =
                service.Login(model.Account, model.Password);

            if (response.IsSuccess)
            {
                // 記住登入成功的會員，
                var rememberMe = false;

                var member = repo.GetByAccount(model.Account);
                string roles = String.Empty;

                var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, member.Account),
            new Claim(ClaimTypes.Role, roles),
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = rememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30),
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

                var url = LocalRedirect("/Home/Index");
                return url;
            }

            ModelState.AddModelError(string.Empty, response.ErrorMessage);

            return this.View(model);
        }


        public async Task<IActionResult> LogOut()
        {
            
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            var url = LocalRedirect("/Staffs/Login");
            return url;
        }
    }
}