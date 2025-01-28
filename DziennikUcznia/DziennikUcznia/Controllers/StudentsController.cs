using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DziennikUcznia.Data;
using DziennikUcznia.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using DziennikUcznia.Repositories;
using DziennikUcznia.Models.View_Models;
using DziennikUcznia.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using DziennikUcznia.Services;
using DziennikUcznia.Interfaces.Services;
using DziennikUcznia.Interfaces.Repositories;
namespace DziennikUcznia.Controllers
{
    public class StudentsController : Controller
    {
        IAddGradesService _addGradesService;
        IStudentsRepository _studentsRepository;
        IClassesRepository _classesRepository;
        IAddStudentService _addStudentService;
        public StudentsController(IStudentsRepository studentsRepository,
            IClassesRepository classesRepository,IAddGradesService gradesService,
            IAddStudentService addStudentService
            )
        {
            _studentsRepository = studentsRepository;
            _classesRepository = classesRepository;
            _addGradesService= gradesService;
            _addStudentService= addStudentService;
        }
        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _studentsRepository.GetStudents());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentsRepository.GetStudentWithGradesById(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public async Task <IActionResult> Create()
        {

            List<SchoolClass> classes = await _classesRepository.GetClasses();
            List<SelectListItem> selectList = new List<SelectListItem>();
            for(int i=0;i<classes.Count; i++)
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
        public async Task<IActionResult> Create([Bind("FirstName,LastName,ClassId")] CreateStudentModel student)
        {
            if (ModelState.IsValid)
            {
                string email = $"{student.FirstName}{student.LastName}@gmail.com";
                AppUser user= await _addStudentService.AddSudentAppUser(email);
                Student st = new Student(student);
                st.UserId = user;
                if (student.ClassId != null)
                {
                    SchoolClass? studentClass = await _classesRepository.GetClassById(student.ClassId.Value);
                    st.Class = studentClass;
                }
                await _studentsRepository.AddStudent(st);
                return RedirectToAction(nameof(Index));
            }
            
            return View(student);
        }
        [AuthorizeRole(IdentityRoles.Role.TEACHER)]
        public  IActionResult ShowAddGrade(int? id)
        {
            ViewBag.StudentId = id;
            return View("AddGrade");
        }
        [AuthorizeRole(IdentityRoles.Role.TEACHER)]
        [HttpPost]
        public async Task<IActionResult> AddGrade(int? id, [Bind("Value,Type")] AddGradeModel modelGrade)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(modelGrade);
            }
            var teacherUserAppid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool success= await _addGradesService.AddGrade(id.Value, modelGrade, teacherUserAppid);
            if(success)
            {
                return RedirectToAction(nameof(Index));
            }
            else return View(modelGrade);
            //if (student == null)
            //{
            //    return NotFound();
            //}
            //Grade grade = new Grade(modelGrade);
            //grade.Student = student;
            //grade.Teacher = teacher;
            //if (ModelState.IsValid)
            //{
            //    await _gradesRepository.AddGrade(grade);
                
            //}
            //return View(modelGrade);
        }
        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentsRepository.GetStudentById(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _studentsRepository.UpdateStudent(student);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_studentsRepository.StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentsRepository.GetStudentById(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _studentsRepository.GetStudentById(id);
            if (student != null)
            {
                await _studentsRepository.RemoveStudent(student);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
