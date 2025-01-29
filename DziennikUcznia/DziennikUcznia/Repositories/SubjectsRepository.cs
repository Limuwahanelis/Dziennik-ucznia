using DziennikUcznia.Data;
using DziennikUcznia.Interfaces.Repositories;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Repositories
{
    public class SubjectsRepository : ISubjectsRepository
    {
        private SchoolDBContext _context;

        public SubjectsRepository(SchoolDBContext dbContext)
        {
            _context = dbContext;
        }
        public async Task AddSubject(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubject(Subject subject)
        {
             _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }

        public async Task<Subject?> GetSubjectById(int id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public async Task<Subject?> GetSubjectByName(string name)
        {
            return await _context.Subjects.Where(s=>s.Name==name).FirstOrDefaultAsync();
        }

        public async Task<List<Subject>> GetSubjects()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task UpdateSubject(Subject subject)
        {
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
        }
    }
}
