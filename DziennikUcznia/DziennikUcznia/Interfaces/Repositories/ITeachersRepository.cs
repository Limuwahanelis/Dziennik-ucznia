using DziennikUcznia.Identity;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Interfaces.Repositories
{
    public interface ITeachersRepository
    {
        public Task AddTeacher(Teacher teacher);
        public Task<List<Teacher>> GetTeachers();
        public Task<Teacher> GetTeacherByAppUser(AppUser user);
    }
}
