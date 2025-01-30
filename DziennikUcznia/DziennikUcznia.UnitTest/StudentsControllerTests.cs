using DziennikUcznia.Controllers;
using DziennikUcznia.Interfaces.Repositories;
using DziennikUcznia.Interfaces.Services;
using DziennikUcznia.Models.View_Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DziennikUcznia.Models;

namespace DziennikUcznia.UnitTest
{
    public class StudentsControllerTests
    {
        [Fact]
        public async void AddGrade_ReturnsRedirectToActionResult()
        {
            IAddGradesService addGradesService = Substitute.For<IAddGradesService>();
            IStudentsRepository studentsRepository = Substitute.For<IStudentsRepository>();
            IClassesRepository classesRepository = Substitute.For<IClassesRepository>();
            ISubjectsRepository subjectsRepository = Substitute.For<ISubjectsRepository>();
            ClaimsPrincipal principal = Substitute.For<ClaimsPrincipal>();
            IAddStudentService addStudentService = Substitute.For<IAddStudentService>();

            Subject subject = new Subject() { Name = "English" };
            ControllerContext controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            subjectsRepository.GetSubjectById(default).ReturnsForAnyArgs(subject);
            AddGradeModel model = new AddGradeModel() { Value = 3, Type = Models.Grade.GradeType.HOMEWORK };
            Claim claim = new Claim("test", "23");
            controllerContext.HttpContext.User = principal;

            principal.FindFirst("2").ReturnsForAnyArgs(claim);
            addGradesService.AddGrade(1, model, "23",subject).Returns(true);

            StudentsController studentsController = new StudentsController(studentsRepository, classesRepository, addGradesService, addStudentService,subjectsRepository)
            {
                ControllerContext = controllerContext
            };

            IActionResult result = await studentsController.AddGrade(1, model);
             Assert.IsType<RedirectToActionResult>(result);
            
        }

    }
}
