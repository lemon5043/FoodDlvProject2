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
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using System.Drawing.Drawing2D;

namespace FoodDlvProject2.Controllers
{
    [Authorize(Roles = "Administrator,PowerUser")]
    public class StaffsController : Controller
    {

    //精靈做的
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment webHostEnvironment;

        //自定義
        private StaffService service;
        private IStaffRepository repo;

        public StaffsController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
            repo = new StaffRepository();
            service = new StaffService(repo);
        }

        // GET: Staffs
        public async Task<IActionResult> Index()
        {

            var data = await _context.Staffs.Select(x => new StaffVM
            {
                Id= x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Title = x.Title,
                Role = x.Role,
                RegistrationTime = x.RegistrationTime,
                Email = x.Email,
                Birthday = x.Birthday,
                Photo = x.Photo,
            }).ToListAsync();
            return View(data);
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
		public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Account,EncryptedPassword,Title,Role,RegistrationTime,Email,birthday")] Staff staff)
		{
			if (ModelState.IsValid)
			{
				_context.Add(staff);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(staff);
		}

		// GET: Staffs/Create
		//public IActionResult Create()
		//{
		//    return View();
		//}

		//// POST: Staffs/Create
		//// To protect from overposting attacks, enable the specific properties you want to bind to.
		//// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Account,EncryptedPassword,Title,Role,RegistrationTime,Photo,Email,Birthday")] StaffVM model)
		//{
		//        string uniqueFileName = await UploadedFile(model);

		//        var staff = new StaffVM
		//        {
		//            FirstName = model.FirstName,
		//            LastName = model.LastName,
		//            Account = model.Account,
		//            EncryptedPassword = model.EncryptedPassword,
		//            Title = model.Title,
		//            Role = model.Role,
		//            RegistrationTime = DateTime.Now,
		//            Email = model.Email,
		//            Birthday = model.Birthday,
		//            Photo = uniqueFileName
		//        };
		//        _context.Add(staff);
		//        await _context.SaveChangesAsync();
		//        return RedirectToAction(nameof(Index));
		//}

		//public async Task<string> UploadedFile(StaffVM model)
  //      {
  //          string uniqueFileName = null;

  //              //save image to wwwroot/Image/Staff
  //              string wwwRootPath = webHostEnvironment.WebRootPath;
  //              string fileName = Path.GetFileNameWithoutExtension(model.PhotoFile.FileName);
  //              string extension = Path.GetExtension(model.PhotoFile.FileName);
  //              uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName + extension;
  //              string path = Path.Combine(wwwRootPath + "/img/", fileName);
  //              using (var fileStream = new FileStream(path, FileMode.Create))
  //              {
  //                  await model.PhotoFile.CopyToAsync(fileStream);
  //              }
  //          return uniqueFileName;
  //      }

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Account,EncryptedPassword,Title,Role,RegistrationTime,Photo,Email,Birthday")] Staff staff)
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
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            return await Task.Run(() => View());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(StaffLoginVM model)
        {
           
            // var service = new MemberService(repository);
            (bool IsSuccess, string? ErrorMessage) response =
                service.Login(model.Account, model.Password);

            if (response.IsSuccess)
            {
                // 記住登入成功的會員，
                var rememberMe = true;

                var member = repo.GetByAccount(model.Account);
                string roles = member.Role;

                var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, member.FirstName),
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

        [AllowAnonymous]
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