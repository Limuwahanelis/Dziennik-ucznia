using DziennikUcznia.Interfaces.Repositories;
using DziennikUcznia.Models;
using DziennikUcznia.Models.View_Models;
using DziennikUcznia.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;

namespace DziennikUcznia.Controllers
{
    public class TeachersController : Controller
    {
        private ITeachersRepository _teachersRepository;
        private IClassesRepository _classesRepository;
        public TeachersController(ITeachersRepository teachersRepository, IClassesRepository classesRepository)
        {
            _teachersRepository = teachersRepository;
            _classesRepository = classesRepository;
        }
        // GET: TeachersController
        public async Task<ActionResult> Index()
        {
            return View(await _teachersRepository.GetTeachers());
        }

        // GET: TeachersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TeachersController/Create
        public async Task<ActionResult> Create()
        {
            List<Class> classes = await _classesRepository.GetClasses();
            List<SelectListItem> selectList = new List<SelectListItem>();
            for (int i = 0; i < classes.Count; i++)
            {
                selectList.Add(new SelectListItem(classes[i].Name, classes[i].Id.ToString()));
            }

            ViewBag.Classes = selectList;
            return View();
        }
        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,ClassesId")] CreateTeacherModel teacher)
        {
            if (ModelState.IsValid)
            {
                Teacher t = new Teacher(teacher);

                List<Class>? teacherClasses = await _classesRepository.GetClassesByIds(teacher.ClassesId);
                t.Classes = teacherClasses;
                await _teachersRepository.AddTeacher(t);
                return RedirectToAction(nameof(Index));
            }

            return View(teacher);
        }

        // GET: TeachersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TeachersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TeachersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TeachersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
