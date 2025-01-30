using DziennikUcznia.Identity;
using DziennikUcznia.Models.View_Models;
using DziennikUcznia.Models;
using DziennikUcznia.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DziennikUcznia.Interfaces.Repositories;
using NuGet.Protocol.Core.Types;
using DziennikUcznia.Interfaces.Services;

namespace DziennikUcznia.Services
{
    public class AddGradesService: IAddGradesService
    {
        private IStudentsRepository _studentsRepository;
        private UserManager<AppUser> _userManager;
        private ITeachersRepository _teachersRepository;
        private IGradesRepository _gradesRepository;
        public AddGradesService( IStudentsRepository studentsRepository, IGradesRepository gradesRepository,
             UserManager<AppUser> userManager, ITeachersRepository teachersRepository)
        {
            _studentsRepository = studentsRepository;
            _gradesRepository = gradesRepository;
            _userManager = userManager;
            _teachersRepository = teachersRepository;
        }
        public async Task<bool> AddGrade(int studentId, AddGradeModel modelGrade, string tacherAppUserId,Subject subject)
        {

            var student = await _studentsRepository.GetStudentById(studentId);
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser user = await _userManager.FindByIdAsync(tacherAppUserId);
            var teacher = await _teachersRepository.GetTeacherByAppUser(user);
            if (student == null)
            {
                return false;
            }
            Grade grade = new Grade(modelGrade);
            grade.Subject = subject;
            grade.Student = student;
            grade.Teacher = teacher;
            await _gradesRepository.AddGrade(grade);
            return true;
        }
    }
}
