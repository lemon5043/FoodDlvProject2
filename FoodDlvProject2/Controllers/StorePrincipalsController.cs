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
using FoodDlvProject2.Models.DTOs;
using FoodDlvProject2.Models.Repositories;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.Services;

namespace FoodDlvProject2.Controllers
{
    public class StorePrincipalsController : Controller
    {
        private IStorePrincipalsRepository repository;
        public StorePrincipalsController()
        {
            repository = new StorePrincipalsRepository();
        }

        // GET: StorePrincipals
        public async Task<IActionResult> Index()
        {
            var foodDeliveryContext = repository.GetAll();

            return View(await foodDeliveryContext);
        }

        // GET: StorePrincipals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                StorePrincipal storePrincipal = new StorePrincipalService(repository).Find(id.Value);
                return View(storePrincipal);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // GET: StorePrincipals/Create
        public IActionResult Create()
        {
            ViewData["AccountStatusId"] = new SelectList(repository.GetAccountStatues(), "Id", "Status");
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
                if (ModelState.IsValid)
                {
                    var storePrincipalCreateDTO = storePrincipalCreateVM.StorePrincipalCreateToDTO();
                    new StorePrincipalService(repository).CreateStorePrincipal(storePrincipalCreateDTO);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            };

            ViewData["AccountStatusId"] = new SelectList(repository.GetAccountStatues(), "Id", "Status", storePrincipalCreateVM.AccountStatusId);
            return View(storePrincipalCreateVM);
        }

        //GET: StorePrincipals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || repository.GetAll() == null)
            {
                return NotFound();
            }
            var storePrincipalInDb = repository.FindById(id);
            if (storePrincipalInDb == null)
            {
                return NotFound();
            }
            StorePrincipalEditVM storePrincipalEditVM = storePrincipalInDb.ToStorePrincipalEditVM();
            ViewData["AccountStatusId"] = new SelectList(repository.GetAccountStatues(), "Id", "Status", storePrincipalEditVM.AccountStatusId);
            return View(storePrincipalEditVM);
        }

        //POST: StorePrincipals/Edit/5
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
                    var storePrincipalEditDTO = storePrincipalEditVM.StorePrincipalEditToDTO();

                    new StorePrincipalService(repository).EditStorePrincipal(id, storePrincipalEditDTO);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            ViewData["AccountStatusId"] = new SelectList(repository.GetAccountStatues(), "Id", "Status", storePrincipalEditVM.AccountStatusId);
            return View(storePrincipalEditVM);
        }

        // GET: StorePrincipals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || repository.GetAll() == null)
            {
                return NotFound();
            }
            var storePrincipal = repository.FindById(id);
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
            if (repository.GetAll() == null)
            {
                //return Problem("Entity set 'FoodDeliveryContext.StorePrincipals'  is null.");
                return NotFound();

            }
            var storePrincipal = repository.FindById(id);
            if (storePrincipal != null)
            {
                repository.DeleteStorePrincipal(storePrincipal);
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
