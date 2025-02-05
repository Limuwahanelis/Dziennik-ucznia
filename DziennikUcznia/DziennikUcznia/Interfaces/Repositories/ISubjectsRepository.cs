using DziennikUcznia.Models;

namespace DziennikUcznia.Interfaces.Repositories
{
    public interface ISubjectsRepository
    {
        public Task UpdateSubject(Subject subject);
        public Task<List<Subject>> GetSubjects();
        public Task AddSubject(Subject subject);
        public Task DeleteSubject(Subject subject);
        public Task<Subject?> GetSubjectByName(string name);
        public Task<Subject?> GetSubjectById(int id);
    }
}
