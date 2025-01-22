using DziennikUcznia.Data;
using DziennikUcznia.Identity;
using DziennikUcznia.Repositories;
using DziennikUcznia.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SchoolDbContextConnection") ?? throw new InvalidOperationException("Connection string 'SchoolDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<TestService>();
builder.Services.AddDbContext<SchoolDBContext,SchoolDbContext_SQLServer>();

//builder.Services.AddDbContext<SchoolDBContext, SchoolDbContext_MySQL>();


builder.Services.AddDefaultIdentity<AppUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    }
).AddEntityFrameworkStores<SchoolDBContext>().AddDefaultUI();
builder.Services.AddRazorPages();

builder.Services.AddScoped<SchoolRepository>();
builder.Services.AddScoped<TeachersRepository>();
builder.Services.AddScoped<StudentsRepository>();
builder.Services.AddScoped<GradesRepository>();
builder.Services.AddScoped<ClassesRepository>();
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

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");




app.Run();