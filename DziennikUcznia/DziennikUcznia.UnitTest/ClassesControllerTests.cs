using DziennikUcznia.Controllers;
using DziennikUcznia.Interfaces.Repositories;
using DziennikUcznia.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DziennikUcznia.UnitTest
{
    public class ClassesControllerTests
    {
        [Fact] 
        public async void Create_ReturnsRedirectToAction()
        {
            SchoolClass schoolClass = new SchoolClass();
            IClassesRepository classesRepository = Substitute.For<IClassesRepository>();
            classesRepository.GetClassByName(default).ReturnsNull();

            ClassesController classesController = new ClassesController(classesRepository);
            IActionResult result= await classesController.Create(schoolClass);

            Assert.IsType<RedirectToActionResult>(result);
        }
        [Fact]
        public async void Create_ReturnsView()
        {
            SchoolClass schoolClass = new SchoolClass();
            SchoolClass schoolClassToReturn = new SchoolClass();
            IClassesRepository classesRepository = Substitute.For<IClassesRepository>();
            classesRepository.GetClassByName(default).ReturnsForAnyArgs(schoolClassToReturn);

            ClassesController classesController = new ClassesController(classesRepository);
            IActionResult result = await classesController.Create(schoolClass);

            Assert.IsType<ViewResult>(result);
        }

    }
}
