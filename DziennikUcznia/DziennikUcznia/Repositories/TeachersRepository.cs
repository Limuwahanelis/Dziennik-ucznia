using DziennikUcznia.Data;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Repositories
{
    public class TeachersRepository
    {
        private SchoolDbContext_MySQL _context;
        public TeachersRepository(SchoolDbContext_MySQL context)
        {
            _context = context;
        }

        public async Task AddTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Teacher>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

    }
}
