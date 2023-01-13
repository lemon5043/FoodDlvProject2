using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDlvProject2.EFModels;
using System.Security.Principal;
using FoodDlvProject2.Models.ViewModels;
using FoodDlvProject2.Models.Infrastructures.ExtensionMethods;

namespace FoodDlvProject2.Controllers
{
    public class StorePrincipalsController : Controller
    {
        private readonly AppDbContext _context;

        public StorePrincipalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StorePrincipals
        public async Task<IActionResult> Index()
        {
            var foodDeliveryContext = _context.StorePrincipals.Include(s => s.AccountStatus);

            return View(await foodDeliveryContext.ToListAsync());
        }

        // GET: StorePrincipals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StorePrincipals == null)
            {
                return NotFound();
            }

            var storePrincipal = await _context.StorePrincipals
                .Include(s => s.AccountStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storePrincipal == null)
            {
                return NotFound();
            }

            return View(storePrincipal);
        }

        // GET: StorePrincipals/Create
        public IActionResult Create()
        {
            ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Status");
            return View();
        }

        // POST: StorePrincipals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StorePrincipalCreateVM storePrincipalCreateVM)
        {
            //AppDbContext _context2 = new AppDbContext();
            try
            {
                StorePrincipal storePrincipal = storePrincipalCreateVM.ToStorePrincipal();
                var emailExist = _context.StorePrincipals.FirstOrDefault(x => x.Email == storePrincipal.Email);
                var accountExist = _context.StorePrincipals.FirstOrDefault(x => x.Account == storePrincipal.Account);
                

                if (emailExist != null) // 表示資料表有這筆記錄
                {
                    throw new Exception("Email已經報名過了, 無法再度報名");
                }
                if (accountExist != null) // 表示資料表有這筆記錄
                {
                    throw new Exception("帳號已經報名過了, 請更改帳號註冊");
                }





                if (ModelState.IsValid)
                {
                    storePrincipal = storePrincipalCreateVM.ToStorePrincipal();
                    storePrincipal.RegistrationTime = DateTime.Now;

                    _context.Add(storePrincipal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }


            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            };


            ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Status", storePrincipalCreateVM.AccountStatusId);
            return View(storePrincipalCreateVM);
        }

        // GET: StorePrincipals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null || _context.StorePrincipals == null)
            //{
            //    return NotFound();
            //}

            //var storePrincipal = await _context.StorePrincipals.FindAsync(id);
            //if (storePrincipal == null)
            //{
            //    return NotFound();
            //}


            var storePrincipalInDb= await _context.StorePrincipals.FindAsync(id);
            if (storePrincipalInDb == null)
            {
                return NotFound();
            }

            StorePrincipalEditVM storePrincipalEditVM= storePrincipalInDb.ToStorePrincipalEditVM();


            ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Status", storePrincipalEditVM.AccountStatusId);
            //StorePrincipalVM storePrincipalVM = storePrincipal.ToStorePrincipaVM();
            return View(storePrincipalEditVM);
        }

        // POST: StorePrincipals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StorePrincipalEditVM storePrincipalEditVM)
        {
            try 
            {
                if (ModelState.IsValid)
                {

                    //try
                    //{
                    //    storePrincipal.RegistrationTime = _context2.StorePrincipals.Find(id).RegistrationTime;

                    //    _context.Update(storePrincipal);
                    //    await _context.SaveChangesAsync();
                    //}
                    //catch (DbUpdateConcurrencyException)
                    //{
                    //    if (!StorePrincipalExists(storePrincipal.Id))
                    //    {
                    //        return NotFound();
                    //    }
                    //    else
                    //    {
                    //        throw;
                    //    }
                    //}



                    //必須呼叫另一個資料庫
                    AppDbContext _context2 = new AppDbContext();


                    StorePrincipal storePrincipal = storePrincipalEditVM.ToStorePrincipal();
                    storePrincipal.Account = _context2.StorePrincipals.Find(id).Account;
                    storePrincipal.Password = _context2.StorePrincipals.Find(id).Password;
                    storePrincipal.RegistrationTime = _context2.StorePrincipals.Find(id).RegistrationTime;

                    var emailExist = _context.StorePrincipals.FirstOrDefault(x => x.Email == storePrincipal.Email);

                    if (emailExist != null) // 表示資料表有這筆記錄
                    {
                        throw new Exception("Email已經報名過了,請更改");
                    }
                    _context.Update(storePrincipal);
                    await _context.SaveChangesAsync();


                    //try
                    //{

                    //    var emailExist = _context.StorePrincipals.FirstOrDefault(x => x.Email == storePrincipal.Email);

                    //    if (emailExist != null) // 表示資料表有這筆記錄
                    //    {
                    //        throw new Exception("Email已經報名過了, 無法再度報名");
                    //    }


                    //    _context.Update(storePrincipal);
                    //    await _context.SaveChangesAsync();
                    //}
                    //catch (DbUpdateConcurrencyException)
                    //{
                    //    if (!StorePrincipalExists(storePrincipal.Id))
                    //    {
                    //        return NotFound();
                    //    }
                    //    else
                    //    {
                    //        throw;
                    //    }
                    //}

                    return RedirectToAction(nameof(Index));
                }

            }
            catch(Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }



           
            ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Status", storePrincipalEditVM.AccountStatusId);
            return View(storePrincipalEditVM);
        }

        // GET: StorePrincipals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StorePrincipals == null)
            {
                return NotFound();
            }

            var storePrincipal = await _context.StorePrincipals
                .Include(s => s.AccountStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storePrincipal == null)
            {
                return NotFound();
            }

            return View(storePrincipal);
        }

        // POST: StorePrincipals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StorePrincipals == null)
            {
                return Problem("Entity set 'FoodDeliveryContext.StorePrincipals'  is null.");
            }
            var storePrincipal = await _context.StorePrincipals.FindAsync(id);
            if (storePrincipal != null)
            {
                _context.StorePrincipals.Remove(storePrincipal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StorePrincipalExists(int id)
        {
            return _context.StorePrincipals.Any(e => e.Id == id);
        }
    }
}
