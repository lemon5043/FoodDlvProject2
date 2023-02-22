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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

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

        [HttpPost]
        public async Task<string> Login(LoginVM model)
        {
            LoginResponse response = await deliveryDriverService.Login(model.Account, model.Password);

            if (response.IsSuccess)
            {
                // 記住登入成功的會員，
                var rememberMe = true;

                var member = deliveryDriverService.GetByAccount(model.Account);
                //string roles = member.Role;

                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name, member.FirstName),
                     //new Claim(ClaimTypes.Role, roles),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = rememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(3),
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

                var url = LocalRedirect("/Home/Index");
                return url.ToString();
            }

            ModelState.AddModelError(string.Empty, response.ErrorMessage);

            return response.ErrorMessage;
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
