using FoodDlvProject2.EFModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FoodDlvProject2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //以下為身分驗證相關 cookie 設置功能
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

            // Add services to the container.

            var FoodDeliveryConnectionString = builder.Configuration.GetConnectionString("FoodDelivery");
			builder.Services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(FoodDeliveryConnectionString));


			builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //以下為驗證相關功能，請洽https://learn.microsoft.com/zh-tw/aspnet/core/security/authentication/cookie?view=aspnetcore-7.0
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}