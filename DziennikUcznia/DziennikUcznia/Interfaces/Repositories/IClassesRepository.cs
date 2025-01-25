using DziennikUcznia.Data;
using DziennikUcznia.Models;

namespace DziennikUcznia.Interfaces.Repositories
{
    public interface IClassesRepository
    {
        public Task<List<Class>> GetClasses();
        public Task<Class?> GetClassById(int id);

        public Task<List<Class>> GetClassesByIds(List<int> ids);
        public Task AddClass(string name);
    }
}
