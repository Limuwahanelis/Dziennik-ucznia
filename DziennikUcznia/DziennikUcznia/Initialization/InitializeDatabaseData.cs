using DziennikUcznia.Data;
using DziennikUcznia.Identity;
using DziennikUcznia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace DziennikUcznia.Initialization
{
    public static class InitializeDatabaseData
    {
        public static async Task Initialize(UserManager<AppUser> userManager,SchoolDBContext context)
        {

                string email = "admin@admin.com";
                string password = "Password@123";
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    AppUser user1 = new AppUser();
                    user1.UserName = email;
                    user1.Email = email;

                    await userManager.CreateAsync(user1, password);
                    await userManager.AddToRoleAsync(user1, IdentityRoles.Role.ADMIN.ToString());
                }
            //SchoolDBContext context = scope.ServiceProvider.GetRequiredService<SchoolDBContext>();

            //AppUser appUser = new AppUser("ADAM NOWAK");
            email = "Adam@Nowak.com";
            password = "Password@12";
            AppUser user = new AppUser();
            if (await userManager.FindByEmailAsync(email) == null)
            {
                user.UserName = email;
                user.Email = email;

                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, IdentityRoles.Role.ADMIN.ToString());
            }

            if (context.Teachers.Where(x => x.FirstName == "Adam" && x.LastName == "Nowak").FirstOrDefault() == null)
            {
                context.Database.Migrate();

                Teacher teacher1 = new Teacher();
                teacher1.FirstName = "Adam";
                teacher1.LastName = "Nowak";
                teacher1.UserId = user;

                context.Teachers.Add(teacher1);
                context.SaveChanges();
            }
            
        }

    }
}
