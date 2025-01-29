using DziennikUcznia.Data;
using DziennikUcznia.Identity;
using DziennikUcznia.Initialization;
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

builder.Services.AddScoped<ITeachersRepository,TeachersRepository>();
builder.Services.AddScoped<IStudentsRepository,StudentsRepository>();
builder.Services.AddScoped<IGradesRepository,GradesRepository>();
builder.Services.AddScoped<IClassesRepository,ClassesRepository>();
builder.Services.AddScoped<ISubjectsRepository,SubjectsRepository>();
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
using (IServiceScope scope = app.Services.CreateScope())
{
    UserManager<AppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    SchoolDBContext context = scope.ServiceProvider.GetRequiredService<SchoolDBContext>();

    await InitializeDatabaseData.Initialize(userManager,context);
}

app.Run();