using DziennikUcznia.Identity;
using DziennikUcznia.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DziennikUcznia.Interfaces.Repositories;
using NSubstitute;
using DziennikUcznia.Models;
using DziennikUcznia.Models.View_Models;
using DziennikUcznia.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NSubstitute.ReturnsExtensions;
namespace DziennikUcznia.UnitTest
{
    public class AddGradeServiceTests
    {
        [Fact]
        public async void AddGrade_ReturnsTrue()
        {
            Mock<UserManager<AppUser>> userManagerMock = new Mock<UserManager<AppUser>>(
                new Mock<IUserStore<AppUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<AppUser>>().Object,
                new IUserValidator<AppUser>[0],
                new IPasswordValidator<AppUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<AppUser>>>().Object
                );

            IStudentsRepository studentsRepository = Substitute.For<IStudentsRepository>();
            ITeachersRepository teachersRepository = Substitute.For<ITeachersRepository>();
            IGradesRepository gradesRepository = Substitute.For<IGradesRepository>();

            Student student = new Student();
            AppUser appUser = new AppUser();
            Teacher teacher = new Teacher();
            Task task = Task.CompletedTask;
            AddGradeModel gradeModel = new AddGradeModel() { Value = 3, Type = Grade.GradeType.HOMEWORK };

            userManagerMock.Setup(m => m.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(appUser);
            studentsRepository.GetStudentById(default).ReturnsForAnyArgs(student);
            teachersRepository.GetTeacherByAppUser(default).ReturnsForAnyArgs(teacher);
            gradesRepository.AddGrade(default).ReturnsForAnyArgs(task);

            AddGradesService addGradesService = new AddGradesService(studentsRepository, gradesRepository, userManagerMock.Object, teachersRepository);
            bool result = await addGradesService.AddGrade(default, gradeModel, default);
            Assert.True(result);
        }

    }
}

