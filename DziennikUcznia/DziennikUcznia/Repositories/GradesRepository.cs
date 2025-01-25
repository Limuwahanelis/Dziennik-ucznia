using DziennikUcznia.Data;
using DziennikUcznia.Interfaces.Repositories;
using DziennikUcznia.Models;

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
    }
}
