using DziennikUcznia.Data;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Repositories
{
    public class GradesRepository
    {
        private SchoolDbContext_MySQL _context;

        public GradesRepository(SchoolDbContext_MySQL context)
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
