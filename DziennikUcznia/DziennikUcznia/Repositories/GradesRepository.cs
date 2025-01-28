using DziennikUcznia.Data;
using DziennikUcznia.Interfaces.Repositories;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Repositories
{
    public class GradesRepository:IGradesRepository
    {
        private SchoolDBContext _context;

        public GradesRepository(SchoolDBContext context)
        {
            _context = context;
        }
        public async Task AddGrade(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
        }

        public async Task<Grade> GetGradeById(int id)
        {
            return await _context.Grades.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Grade>> GetGradesByStudent(Student student)
        {
            return await _context.Grades.Where(g => g.Student == student).ToListAsync();
        }
    }
}
