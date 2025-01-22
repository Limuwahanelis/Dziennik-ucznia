using DziennikUcznia.Data;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Repositories
{
    public class SchoolRepository
    {
        private readonly SchoolDBContext _context;

        public SchoolRepository(SchoolDBContext context)
        {
            _context = context;
        }

        public void TestDb()
        {
            _context.Database.EnsureCreated();
            Student st1 = new Student() {FirstName ="Darek", LastName="Larski" };
            //Grade grade = new Grade() {Student=st1,Type=Grade.GradeType.TEST };
            _context.Students.Add(st1);
            _context.SaveChanges();
        }
        public void CreateDb()
        {
            _context.Database.Migrate();
            Student st1 = new Student() { FirstName = "Darek", LastName = "Larski" };
            Grade grade = new Grade() { Student = st1, Type = Grade.GradeType.TEST };
            _context.Students.Add(st1);
            _context.SaveChanges();
        }


    }
}
