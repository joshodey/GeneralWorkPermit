using GeneralWorkPermit.Configuration;
using GeneralWorkPermit.Context;
using GeneralWorkPermit.EmailService;
using GeneralWorkPermit.Implementation;
using GeneralWorkPermit.Models;
using GeneralWorkPermit.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseSqlite("DataSource = GeneralWorkPermit"));

builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddTransient<IReview, ReviewRepo>();
builder.Services.AddScoped<IRepository<Applicants>, Repository<Applicants>>();

builder.Services.ConfigureMailService(config);
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(config);


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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
