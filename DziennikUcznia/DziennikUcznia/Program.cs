using DziennikUcznia.Data;
using DziennikUcznia.Identity;
using DziennikUcznia.Interfaces.Repositories;
using DziennikUcznia.Interfaces.Services;
using DziennikUcznia.Repositories;
using DziennikUcznia.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SchoolDbContextConnection") ?? throw new InvalidOperationException("Connection string 'SchoolDbContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<TestService>();
builder.Services.AddTransient<IAddGradesService,AddGradesService>();
builder.Services.AddDbContext<SchoolDBContext,SchoolDbContext_SQLServer>();

//builder.Services.AddDbContext<SchoolDBContext, SchoolDbContext_MySQL>();


builder.Services.AddDefaultIdentity<AppUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
    }
).AddRoles<IdentityRole>().AddEntityFrameworkStores<SchoolDBContext>().AddDefaultUI();
builder.Services.AddRazorPages();

builder.Services.AddScoped<SchoolRepository>();
builder.Services.AddScoped<ITeachersRepository,TeachersRepository>();
builder.Services.AddScoped<IStudentsRepository,StudentsRepository>();
builder.Services.AddScoped<IGradesRepository,GradesRepository>();
builder.Services.AddScoped<IClassesRepository,ClassesRepository>();
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


using (var scope = app.Services.CreateScope())
{
    RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = Enum.GetNames(typeof(IdentityRoles.Role));

    foreach(string role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
        
    }
}
using (var scope = app.Services.CreateScope())
{
    UserManager<AppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    string email = "admin@admin.com";
    string password = "Password@123";
    if(await userManager.FindByEmailAsync(email)==null)
    {
        AppUser user = new AppUser();
        user.UserName = email;
        user.Email=email;

        await userManager.CreateAsync(user,password);
        await userManager.AddToRoleAsync(user, IdentityRoles.Role.ADMIN.ToString());
    }
}

app.Run();