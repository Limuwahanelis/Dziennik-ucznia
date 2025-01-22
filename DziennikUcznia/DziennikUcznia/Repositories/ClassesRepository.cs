using DziennikUcznia.Data;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Repositories
{
    public class ClassesRepository
    {
        private SchoolDbContext_MySQL _context;
        public ClassesRepository(SchoolDbContext_MySQL context)
        {
            _context = context;
        }
        public async Task<List<Class>> GetClasses()
        {
            return await _context.Classes.ToListAsync();
        }
        public async Task<Class?> GetClassById(int id)
        {
            return await _context.Classes.FindAsync(id);
        }

        public async Task<List<Class>> GetClassesByIds(List<int> ids)
        {
            List<Class> classes = new List<Class>();
            foreach (int id in ids)
            {
                classes.Add(await _context.Classes.FindAsync(id));
            }
            return classes;
        }
    }
}
