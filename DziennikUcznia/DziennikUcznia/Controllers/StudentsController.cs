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
namespace DziennikUcznia.Controllers
{
    public class StudentsController : Controller
    {
        SchoolRepository _repository;
        StudentsRepository _studentsRepository;
        GradesRepository _gradesRepository; 
        ClassesRepository _classesRepository;
        TeachersRepository _teachersRepository;
        UserManager<AppUser> _userManager;
        public StudentsController(SchoolRepository repository, StudentsRepository studentsRepository, GradesRepository gradesRepository,
            ClassesRepository classesRepository,UserManager<AppUser> userManager,TeachersRepository teachersRepository)
        {
            _repository = repository;
            _studentsRepository = studentsRepository;
            _gradesRepository = gradesRepository;
            _classesRepository = classesRepository;
            _userManager = userManager;
            _teachersRepository = teachersRepository;
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
            List<Class> classes = await _classesRepository.GetClasses();
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
                Student st = new Student(student);
                Class? studentClass = await _classesRepository.GetClassById(student.ClassId.Value);
                st.Class = studentClass;
                await _studentsRepository.AddStudent(st);
                return RedirectToAction(nameof(Index));
            }
            
            return View(student);
        }
        [AuthorizeRole(IdentityRoles.Role.TEACHER)]
        public  IActionResult ShowAddGrade(int? id)
        {
            return View("AddGrade");
        }
        [HttpPost]
        public async Task<IActionResult> AddGrade(int? id, [Bind("Value,Type")] Grade grade)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student =await _studentsRepository.GetStudentById(id.Value);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser user = await _userManager.FindByIdAsync(userId);
            var teacher = await _teachersRepository.GetTeacherByAppUser(user);
            if (student == null)
            {
                return NotFound();
            }
            grade.Student = student;
            grade.Teacher = teacher;
            ModelState.Remove("Student");
            ModelState.Remove("Teacher");
            if (ModelState.IsValid)
            {
                await _gradesRepository.AddGrade(grade);
            }
            return View(grade);
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
