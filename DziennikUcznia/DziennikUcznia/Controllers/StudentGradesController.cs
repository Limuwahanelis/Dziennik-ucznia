using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DziennikUcznia.Data;
using DziennikUcznia.Models;
using DziennikUcznia.Identity;
using DziennikUcznia.Interfaces.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace DziennikUcznia.Controllers
{
    [AuthorizeRole(IdentityRoles.Role.STUDENT)]
    public class StudentGradesController : Controller
    {
        //private readonly SchoolDbContext_SQLServer _context;

        private readonly IGradesRepository _gradesRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStudentsRepository _studentsRepository;
        public StudentGradesController(IGradesRepository gradesRepository,IStudentsRepository studentsRepository,UserManager<AppUser> userManager)
        {
           _userManager=userManager;
           _gradesRepository=gradesRepository;
            _studentsRepository=studentsRepository;
        }

        // GET: StudentGrades
        public async Task<IActionResult> Index()
        {
            string studentAppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser user = await _userManager.FindByIdAsync(studentAppUserId);
            Student student= await _studentsRepository.GetStudentByAppUser(user);
            List<Grade> grades = await _gradesRepository.GetGradesByStudent(student);
            return View(grades);
        }

        //// GET: StudentGrades/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var grade = await _context.Grades
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (grade == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(grade);
        //}
    }
}
