
using ExamForScoolchildrenApp.Aplication.Interface;
using ExamForScoolchildrenApp.Aplication.Services;
using ExamForScoolchildrenApp.Aplication.Services.EmailService;
using ExamForScoolchildrenApp.Application.Interface;
using ExamForScoolchildrenApp.Domain.Entities;
using ExamForScoolchildrenApp.Domain.Interfaces;
using ExamForScoolchildrenApp.Infrastructur.Data;
using ExamForScoolchildrenApp.Infrastructur.Repository;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext
builder.Services.AddDbContext<ExamForScoolDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services and repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<LessonService>();
builder.Services.AddScoped<StudentService>();  // If you have similar services for Student and Exam
builder.Services.AddScoped<ExamService>();
builder.Services.AddControllers();

// your EmailSender configuration section
builder.Services.AddSingleton<IEmailSender, Emailsender>(e => new Emailsender(
    builder.Configuration["EmailSettings:Host"],
    builder.Configuration.GetValue<int>("EmailSettings:Port"),
    builder.Configuration.GetValue<bool>("EmailSettings:SSL"),
    builder.Configuration["EmailSettings:Username"],
    builder.Configuration["EmailSettings:Password"]
    ));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    //Password
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 3;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = false;

    //Lock out
    options.Lockout.AllowedForNewUsers = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 4;

    //User
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters =
   "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";

}).AddEntityFrameworkStores<ExamForScoolDBContext>()
       .AddDefaultTokenProviders();


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

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
