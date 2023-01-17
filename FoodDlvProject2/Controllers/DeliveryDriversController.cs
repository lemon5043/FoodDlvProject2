using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using FoodDlvProject2.Models.Services;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.Repositories;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FoodDlvProject2.Controllers
{
    public class DeliveryDriversController : Controller
    {
        private readonly DeliveryDriverService deliveryDriverService;

        public DeliveryDriversController()
        {
            var db = new AppDbContext();
            IDeliveryDriversRepository repository = new DeliveryDriversRepository(db);
            this.deliveryDriverService = new DeliveryDriverService(repository);
        }

        // GET: DeliveryDrivers
        public async Task<IActionResult> Index()
        {
            var data = deliveryDriverService.GetDelivers().Select(x => x.ToDeliveryDriversIndexVM());
            return await Task.Run(() => View(data));
        }

        // GET: DeliveryDrivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                var data = deliveryDriverService.GetOne(id).ToDeliveryDriversDetailsVM();
                return View(data);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            
        }

        // GET: DeliveryDrivers/Create
        //public IActionResult Create()
        //{
        //    ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Id");
        //    ViewData["WorkStatuseId"] = new SelectList(_context.DeliveryDriverWorkStatuses, "Id", "Id");
        //    return View();
        //}

        // POST: DeliveryDrivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,AccountStatusId,WorkStatuseId,FirstName,LastName,Phone,Gender,BankAccount,Idcard,RegistrationTime,VehicleRegistration,Birthday,Email,Account,Password,DriverLicense,Longitude,Latitude")] DeliveryDriver deliveryDriver)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(deliveryDriver);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AccountStatusId"] = new SelectList(_context.AccountStatues, "Id", "Id", deliveryDriver.AccountStatusId);
        //    ViewData["WorkStatuseId"] = new SelectList(_context.DeliveryDriverWorkStatuses, "Id", "Id", deliveryDriver.WorkStatuseId);
        //    return View(deliveryDriver);
        //}

        // GET: DeliveryDrivers/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            var data = deliveryDriverService.GetOne(id).ToDeliveryDriversEditVM();
            if (data == null) return NotFound();
            var selectList = deliveryDriverService.GetList();
            ViewData["AccountStatusId"] = new SelectList(selectList.Item1, "Id", "Status", data.AccountStatusId);
            ViewData["WorkStatuseId"] = new SelectList(selectList.Item2, "Id", "Status", data.WorkStatuseId);
            return View(data);
        }

        // POST: DeliveryDrivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
                     [Bind("Id,AccountStatusId,WorkStatuseId,FirstName,LastName,Phone,Gender,BankAccount," +
                            "Birthday,Email")] 
                     DeliveryDriversEditVM deliveryDriver)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    deliveryDriverService.Edit(deliveryDriver.ToDeliveryDriverEditDTO());
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
                return RedirectToAction(nameof(Index));
            }
            var selectList = deliveryDriverService.GetList();
            ViewData["AccountStatusId"] = new SelectList(selectList.Item1, "Id", "Status", deliveryDriver.AccountStatusId);
            ViewData["WorkStatuseId"] = new SelectList(selectList.Item2, "Id", "Status", deliveryDriver.WorkStatuseId);

            return View(deliveryDriver);
        }

        //// GET: DeliveryDrivers/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.DeliveryDrivers == null)
        //    {
        //        return NotFound();
        //    }

        //    var deliveryDriver = await _context.DeliveryDrivers
        //        .Select(x => new DeliveryDriversDeleteVM
        //        {
        //            Id = x.Id,
        //            Account = x.Account,
        //            DriverName = x.LastName + x.FirstName,
        //            Gender = x.Gender,
        //            Birthday = x.Birthday,
        //            Phone = x.Phone,
        //            Email = x.Email,
        //            BankAccount = x.BankAccount,
        //            AccountStatus = x.AccountStatus.Status,
        //            WorkStatuse = x.WorkStatuse.Status,
        //            DeliveryViolationRecords = x.DeliveryViolationRecords.Sum(x => x.DeliveryDriversId),
        //            DriverRating = x.Orders.Average(x => x.DriverRating),
        //            RegistrationTime = x.RegistrationTime,
        //            Idcard = x.Idcard,
        //            VehicleRegistration = x.VehicleRegistration,
        //            DriverLicense = x.DriverLicense,
        //        })
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (deliveryDriver == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(deliveryDriver);
        //}

        //// POST: DeliveryDrivers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.DeliveryDrivers == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.DeliveryDrivers'  is null.");
        //    }
        //    var deliveryDriver = await _context.DeliveryDrivers.FindAsync(id);
        //    if (deliveryDriver != null)
        //    {
        //        _context.DeliveryDrivers.Remove(deliveryDriver);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool DeliveryDriverExists(int id)
        //{
        //    return _context.DeliveryDrivers.Any(e => e.Id == id);
        //}
    }
}
