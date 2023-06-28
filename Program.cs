using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using bookverse.Data;
using bookverse.Repository;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Builder;
using bookverse.Models;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
 
var connectionString = builder.Configuration.GetConnectionString("DBContextConnection") ?? throw new InvalidOperationException("Connection string 'DBContextConnection' not found.");

builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddDefaultTokenProviders()
    .AddEntityFrameworkStores<DBContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddRazorPages().AddNToastNotifyNoty(new NotyOptions { ProgressBar = true, Timeout = 5000,});

// Add services to the container.

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
app.UseAuthentication();;

app.UseAuthorization();
app.UseNToastNotify();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();