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
            ClaimsPrincipal principal = Substitute.For<ClaimsPrincipal>();
            IAddStudentService addStudentService = Substitute.For<IAddStudentService>();
            ControllerContext controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            AddGradeModel model = new AddGradeModel() { Value = 3, Type = Models.Grade.GradeType.HOMEWORK };
            Claim claim = new Claim("test", "23");
            controllerContext.HttpContext.User = principal;

            principal.FindFirst("2").ReturnsForAnyArgs(claim);
            addGradesService.AddGrade(1, model, "23").Returns(true);

            StudentsController studentsController = new StudentsController(studentsRepository, classesRepository, addGradesService, addStudentService)
            {
                ControllerContext = controllerContext
            };

            IActionResult result = await studentsController.AddGrade(1, model);
             Assert.IsType<RedirectToActionResult>(result);
            
        }

    }
}
