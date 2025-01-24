using DziennikUcznia.Data;
using DziennikUcznia.Identity;
using DziennikUcznia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Repositories
{
    public class SchoolRepository
    {
        private readonly SchoolDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        public SchoolRepository(SchoolDBContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void TestDb()
        {
            _context.Database.EnsureCreated();
            Student st1 = new Student() {FirstName ="Darek", LastName="Larski" };
            AppUser appUser = new AppUser("ADAM NOWAK");
            appUser.Email = "Adam@Nowak.com";
            appUser.UserName = appUser.Email;
            string password = "Password@123";
            _userManager.CreateAsync(appUser, password).Wait();
            _userManager.AddToRoleAsync(appUser,IdentityRoles.Role.TEACHER.ToString()).Wait();
            Teacher teacher1 = new Teacher();
            teacher1.FirstName="Adam";
            teacher1.LastName = "Nowak";
            teacher1.UserId = appUser;
            
            _context.Teachers.Add(teacher1);
            //Grade grade = new Grade() {Student=st1,Type=Grade.GradeType.TEST };
            _context.Students.Add(st1);
            _context.SaveChanges();
        }
        public void CreateDb()
        {
            _context.Database.Migrate();
            Student st1 = new Student() { FirstName = "Darek", LastName = "Larski" };
            Grade grade = new Grade() { Student = st1, Type = Grade.GradeType.TEST };
            st1.Grades.Add(grade);
            _context.Students.Add(st1);
            _context.SaveChanges();
        }


    }
}
