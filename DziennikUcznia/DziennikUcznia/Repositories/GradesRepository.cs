using DziennikUcznia.Data;
using DziennikUcznia.Interfaces;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Repositories
{
    public class GradesRepository
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
