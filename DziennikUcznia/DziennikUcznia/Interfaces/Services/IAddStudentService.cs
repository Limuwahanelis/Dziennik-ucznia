using DziennikUcznia.Identity;

namespace DziennikUcznia.Interfaces.Services
{
    public interface IAddStudentService
    {
        public Task<AppUser> AddSudentAppUser(string email);
    }
}
