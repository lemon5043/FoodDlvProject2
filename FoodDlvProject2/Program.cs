using FoodDlvProject2.Models.EFModels;
using Microsoft.EntityFrameworkCore;

namespace FoodDlvProject2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Orders}/{action=Index}/{id?}");

            app.Run();
        }
    }
}