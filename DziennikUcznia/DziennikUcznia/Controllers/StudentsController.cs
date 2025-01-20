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

namespace DziennikUcznia.Controllers
{
    public class StudentsController : Controller
    {
        SchoolRepository _repository;
        public StudentsController(SchoolRepository repository)
        {
            _repository = repository;
        }
        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetStudents());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _repository.GetStudentWithGradesById(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName")] Student student)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddStudent(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
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

            var student =await _repository.GetStudentById(id.Value);

            if (student == null)
            {
                return NotFound();
            }
            grade.Student = student;
            ModelState.Remove("Student");
            if (ModelState.IsValid)
            {
                await _repository.AddGrade(grade);
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

            var student = await _repository.GetStudentById(id.Value);
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
                    await _repository.UpdateStudent(student);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repository.StudentExists(student.Id))
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

            var student = await _repository.GetStudentById(id.Value);
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
            var student = await _repository.GetStudentById(id);
            if (student != null)
            {
                await _repository.RemoveStudent(student);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
