using DziennikUcznia.Identity;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Interfaces.Repositories
{
    public interface IStudentsRepository
    {
        public Task AddStudent(Student student);
        public Task UpdateStudent(Student student);
        public Task RemoveStudent(Student student);
        public Task<List<Student>> GetStudents();
        public Task<Student?> GetStudentWithGradesById(int id);
        public Task<Student?> GetStudentById(int id);
        public Task<Student?> GetStudentByAppUser(AppUser appUser);

        public bool StudentExists(int id);
    }
}
