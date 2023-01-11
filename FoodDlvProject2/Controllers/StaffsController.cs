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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(StaffLoginVM model)
        {
            // var service = new MemberService(repository);
            (bool IsSuccess, string ErrorMessage) response =
                service.Login(model.Account, model.Password);

            //if (response.IsSuccess)
            //{
            //    // 記住登入成功的會員，要會cookie才能用
            //    var rememberMe = false;

            //    string returnUrl = ProcessLogin(model.Account, rememberMe, out HttpCookie cookie);

            //    Response.Cookies.Add(cookie);

            //    return Redirect(returnUrl);
            //}

            ModelState.AddModelError(string.Empty, response.ErrorMessage);

            return this.View(model);
        }


        //施工中，拎北不會用 cookie
        //private string ProcessLogin(string account, bool rememberMe)
        //{
        //    var member = repo.GetByAccount(account);
        //    string roles = String.Empty; // 在本範例, 沒有用到角色權限,所以存入空白

            //// 建立一張認證票
            //FormsAuthenticationTicket ticket =
            //    new FormsAuthenticationTicket(
            //        1,          // 版本別, 沒特別用處
            //        account,
            //        DateTime.Now,   // 發行日
            //        DateTime.Now.AddDays(2), // 到期日
            //        rememberMe,     // 是否續存
            //        roles,          // userdata
            //        "/" // cookie位置
            //    );

            //// 將它加密
            //string value = FormsAuthentication.Encrypt(ticket);

            //// 存入cookie
            //cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);

            //// 取得return url
            //string url = FormsAuthentication.GetRedirectUrl(account, true); //第二個引數沒有用處

            //return url;
        }


    }
