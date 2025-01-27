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
        public async Task<List<SchoolClass>> GetClasses()
        {
            return await _context.Classes.ToListAsync();
        }
        public async Task<SchoolClass?> GetClassById(int id)
        {
            return await _context.Classes.FindAsync(id);
        }

        public async Task<List<SchoolClass>> GetClassesByIds(List<int> ids)
        {
            List<SchoolClass> classes = new List<SchoolClass>();
            foreach (int id in ids)
            {
                classes.Add(await _context.Classes.FindAsync(id));
            }
            return classes;
        }
        public async Task AddClass(string name)
        {
            SchoolClass newClass = new SchoolClass();
            newClass.Name = name;
            await _context.Classes.AddAsync(newClass);
            _context.SaveChanges();
        }

        public async Task DeleteClass(int id)
        {
            SchoolClass schoolClass = await GetClassById(id);
            if (schoolClass != null)
            {
                _context.Classes.Remove(schoolClass);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ChangeClassName(int id, string newClassName)
        {
            SchoolClass schoolClass = await GetClassById(id);
            if (schoolClass != null) 
            {
                schoolClass.Name= newClassName;
                await _context.SaveChangesAsync();
            }
        }
    }
}
