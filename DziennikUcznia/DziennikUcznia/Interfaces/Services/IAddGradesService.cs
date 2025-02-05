using DziennikUcznia.Models;
using DziennikUcznia.Models.View_Models;

namespace DziennikUcznia.Interfaces.Services
{
    public interface IAddGradesService
    {
        public Task<bool> AddGrade(int studentId, AddGradeModel modelGrade, string tacherAppUserId,Subject subject);
    }
}
