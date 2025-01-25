using DziennikUcznia.Data;
using DziennikUcznia.Identity;
using DziennikUcznia.Interfaces.Repositories;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Repositories
{
    public class TeachersRepository:ITeachersRepository
    {
        private SchoolDBContext _context;
        public TeachersRepository(SchoolDBContext context)
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
        public async Task<Teacher> GetTeacherByAppUser(AppUser user)
        {
            return await _context.Teachers.Where(t=>t.UserId==user).FirstOrDefaultAsync();
        }
    }
}
