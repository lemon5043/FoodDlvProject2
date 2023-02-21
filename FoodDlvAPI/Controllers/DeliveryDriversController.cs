using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Components.Forms;
using FoodDlvAPI.Models.Services;
using FoodDlvAPI.Models.Services.Interfaces;
using FoodDlvAPI.Models.Repositories;
using FoodDlvAPI.Models;
using FoodDlvAPI.Models.ViewModels;
using FoodDlvAPI.Models.DTOs;

namespace FoodDlvAPI.Controllers
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

        public async Task<bool> Login(LoginVM model)
        {
            LoginResponse response = deliveryDriverService.Login(model.Account, model.Password);

            return await deliveryDriverService.Login(model.Account,model.Password);
        }

        // GET: DeliveryDrivers/Details/5
        public async Task<DeliveryDriversDetailsVM> Details(int? id)
        {
            var data = await deliveryDriverService.GetOneAsync(id);
            return data.ToDeliveryDriversDetailsVM();
        }

        // GET: DeliveryDrivers/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: DeliveryDrivers/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> Create(DeliveryDriverCreateVM deliveryDriver)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await deliveryDriverService.CreateAsync(deliveryDriver.ToDeliveryDriverEditDTO());
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            return "輸入資料有誤，請再確認";
        }

        // GET: DeliveryDrivers/Edit/5

        public async Task<DeliveryDriversEditVM> Edit(int? id)
        {
            var data = await deliveryDriverService.GetEditAsync(id);
            return data.ToDeliveryDriversEditVM();
        }

        // POST: DeliveryDrivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> Edit(DeliveryDriversEditVM deliveryDriver)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await deliveryDriverService.EditAsync(deliveryDriver.ToDeliveryDriverEntity());
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            return "輸入資料有誤，請再確認";
        }


    }
}
