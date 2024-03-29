using FoodApplication.ContextDBConfig;
using Microsoft.EntityFrameworkCore;
using FoodApplication.Models;
using Microsoft.AspNetCore.Identity;
using FoodApplication.Repository;


var builder = WebApplication.CreateBuilder(args);
var dbConnection = builder.Configuration.GetConnectionString("dbConnection");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FoodApplicationDBContext>(options => 
options.UseSqlServer(dbConnection));//have to be here

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<FoodApplicationDBContext>();
// Configure the HTTP request pipeline.
builder.Services.AddTransient<IData, Data>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
