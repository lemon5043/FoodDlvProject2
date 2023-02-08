using FoodDlvProject2.EFModels;
using FoodDlvProject2.Models.Repositories;
using FoodDlvProject2.Models.Services.Interfaces;
using FoodDlvProject2.Models.Services;
using FoodDlvProject2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace FoodDlvProject2.Controllers
{
    public class DeliveryViolationRecordsController : Controller
    {
        private readonly DeliveryViolationRecordService deliveryViolationRecordService;

        public DeliveryViolationRecordsController()
        {
            var db = new AppDbContext();
            IDeliveryViolationRecordsRepository repository = new DeliveryViolationRecordsRepository(db);
            this.deliveryViolationRecordService = new DeliveryViolationRecordService(repository);
        }
        // GET: DeliveryViolationRecords
        public async Task<IActionResult> Index()
        {
            var data = await deliveryViolationRecordService.GetViolationRecords();
            return View(data.Select(x => x.ToDeliveryViolationRecordsIndexVM()));
        }

        // GET: DeliveryViolationRecords/PersonalDetails/5
        public async Task<IActionResult> PersonalDetails(int? id)
        {
            try
            {
                var data = await deliveryViolationRecordService.GetPersonalViolationRecordsAsync(id);
                var VM = data.Select(x => x.ToDeliveryViolationRecordPersonalDetailsVM());

                ViewBag.DriverId = id;
                ViewBag.DriverName = VM.Select(x => x.DriverName).FirstOrDefault();

                return View(VM);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }

        }

        // GET: DeliveryViolationRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                var data = await deliveryViolationRecordService.GetDetailsAsync(id);

                ViewBag.DriverId = id;

                return View(data.ToDeliveryViolationRecordDetailsVM());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: DeliveryViolationRecords/Edit/5
        public async Task<IActionResult> Edit(int? Id)
        {
            return View(await GetEditAsync(Id,null));
        }

        // POST: DeliveryViolationRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,ViolationId,ViolationDate")] DeliveryViolationRecordEditVM DeliveryViolationRecord)
        {
            ModelState.Remove("DriverName");
            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Result"] = await deliveryViolationRecordService.EditAsync(DeliveryViolationRecord.ToDeliveryViolationRecordDTO());
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View(await GetEditAsync(id, DeliveryViolationRecord.ViolationId));
                }

                return RedirectToAction(nameof(Index));
            }

            return View(await GetEditAsync(id, DeliveryViolationRecord.ViolationId));
        }
        // GET: DeliveryViolationRecords/Delete/5

        // GET: Pays/Create
        public async Task<IActionResult> Create()
        {
            var selectList = await deliveryViolationRecordService.GetListAsync();
            ViewData["ViolationId"] =
                new SelectList(selectList.Select(x => x.ToDeliveryViolationTypesVM()), "Id", "ViolationContent");
            return View();
        }

        // POST: Pays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DriverId,OrderId,ViolationId,ViolationDate")] DeliveryViolationRecordCreateVM DeliveryViolationRecord)
        {
            ModelState.Remove("DriverName");
            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Result"] = await deliveryViolationRecordService.CreateAsync(DeliveryViolationRecord.ToDeliveryViolationRecordDTO());
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;

                    await GetCreateListAsync(DeliveryViolationRecord.ViolationId);

                    return View(DeliveryViolationRecord);
                }
                return RedirectToAction(nameof(Index));

            }
            await GetCreateListAsync(DeliveryViolationRecord.ViolationId);

            return View(DeliveryViolationRecord);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var data = await deliveryViolationRecordService.GetDetailsAsync(id);

                return View(data.ToDeliveryViolationRecordDeleteVM());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: DeliveryViolationRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await deliveryViolationRecordService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }       
        }

        private async Task<DeliveryViolationRecordEditVM> GetEditAsync(int? id, int? ViolationId)
        {
            var data = await deliveryViolationRecordService.GetEditAsync(id);
            var VM = data.ToDeliveryViolationRecordEditVM();
            var selectList = await deliveryViolationRecordService.GetListAsync();
            int VId = (int)(ViolationId == null ? VM.ViolationId : ViolationId);
            ViewData["ViolationId"] = new SelectList(selectList.Select(x => x.ToDeliveryViolationTypesVM()), "Id", "ViolationContent", VId);

            return VM;
        }

        private async Task GetCreateListAsync(int ViolationId)
        {
            var selectList = await deliveryViolationRecordService.GetListAsync();
            ViewData["ViolationId"] =
                new SelectList(selectList.Select(x => x.ToDeliveryViolationTypesVM()), "Id", "ViolationContent", ViolationId);
        }
    }
}
