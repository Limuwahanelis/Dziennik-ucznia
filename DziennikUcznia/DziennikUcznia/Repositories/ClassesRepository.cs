using DziennikUcznia.Data;
using DziennikUcznia.Interfaces;
using DziennikUcznia.Interfaces.Repositories;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Repositories
{
    public class ClassesRepository: IClassesRepository
    {
        private SchoolDBContext _context;
        public ClassesRepository(SchoolDBContext context)
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
        public async Task AddClass(string name)
        {
            Class newClass = new Class();
            newClass.Name = name;
            await _context.Classes.AddAsync(newClass);
            _context.SaveChanges();
        }
    }
}
