using FoodDlvAPI.Hubs;
using FoodDlvAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//加入 SignalR
builder.Services.AddSignalR();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//註冊要使用的 database 類別
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("FoodDelivery")));

var app = builder.Build();

// 允許 react app 存取 此 api 的任何資料 
app.UseCors(options =>
options.WithOrigins("http://localhost:5129")
.AllowAnyMethod()
.AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//加入 Hub
app.MapHub<OrderHub>("/OrderHub");


app.Run();
