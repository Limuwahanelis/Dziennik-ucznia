using DziennikUcznia.Data;
using DziennikUcznia.Models;

namespace DziennikUcznia.Interfaces.Repositories
{
    public interface IClassesRepository
    {
        public Task<List<SchoolClass>> GetClasses();
        public Task<SchoolClass?> GetClassById(int id);

        public Task<List<SchoolClass>> GetClassesByIds(List<int> ids);
        public Task AddClass(string name);
        public Task DeleteClass(int id);
        public Task ChangeClassName(int id, string newClassName);
        public Task <SchoolClass?> GetClassByName(string name);
    }
}
