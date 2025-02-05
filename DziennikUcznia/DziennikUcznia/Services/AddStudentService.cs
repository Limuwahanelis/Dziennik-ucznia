using DziennikUcznia.Data;
using DziennikUcznia.Identity;
using DziennikUcznia.Interfaces.Services;
using DziennikUcznia.Models;
using Microsoft.AspNetCore.Identity;

namespace DziennikUcznia.Services
{
    
    public class AddStudentService : IAddStudentService
    {
        private readonly UserManager<AppUser> _userManager;
        public AddStudentService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<AppUser> AddSudentAppUser(string email)
        {
            string password = "Password@123";
            AppUser user = new AppUser();
            user.UserName = email;
            user.Email = email;
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, IdentityRoles.Role.STUDENT.ToString());

            return user;
        }
    }
}
