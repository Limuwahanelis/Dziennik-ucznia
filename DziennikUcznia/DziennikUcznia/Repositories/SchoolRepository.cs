using DziennikUcznia.Data;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Repositories
{
    public class SchoolRepository
    {
        private readonly SchoolDbContext _context;

        public SchoolRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public void TestDb()
        {
            _context.Database.EnsureCreated();
            Student st1 = new Student() {FirstName ="Darek", LastName="Larski" };
            Grade grade = new Grade() {Student=st1,Type=Grade.GradeType.TEST };
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
        public async Task AddStudent(Student student)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Student>> GetStudents()
        {
           return await _context.Students.ToListAsync();
        }
        public async Task<Student?> GetStudentWithGradesById(int id)
        {
            var student = await _context.Students
                .Include(s => s.Grades)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            return student;
        }
        public async Task<Student?> GetStudentById(int id)
        {
            return await _context.Students.FindAsync(id);
        }
        public async Task AddGrade(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStudent(Student student)
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveStudent(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
