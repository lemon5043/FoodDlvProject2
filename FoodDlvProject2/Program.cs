using FoodDlvProject2.EFModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using FoodDlvProject2.Hubs;


namespace FoodDlvProject2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //以下為身分驗證相關 cookie 設置功能
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Staffs/Login";
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Staffs", policy =>
                      policy.RequireRole("Administrator", "PowerUser"));
            });

            // Add services to the container.

            var FoodDeliveryConnectionString = builder.Configuration.GetConnectionString("FoodDelivery");
			builder.Services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(FoodDeliveryConnectionString));


			builder.Services.AddControllersWithViews();
            builder.Services.AddSignalR();

            builder.Services.AddSignalR();

            var app = builder.Build();

            //404頁面
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Home/Error404";
                    await next();
                }
            });

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

            app
            .MapControllers()
            .RequireAuthorization(); // This will set a default policy that says a user has to be authenticated

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"           
                );
            app.MapHub<ChatHub>("/chatHub");
            app.Run();
        }
    }
}