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
using DziennikUcznia.Models.View_Models;
using DziennikUcznia.Services;

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
            string? studentAppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser? user = await _userManager.FindByIdAsync(studentAppUserId);
            Student? student= await _studentsRepository.GetStudentByAppUser(user);
            List<IGrouping<Subject, Grade>> dd = await _gradesRepository.GetStudentGradesGroupedBySubject(student);
            List<List<Grade>> allGrades = new List<List<Grade>>();
            List<Subject> subjects=new List<Subject>();
            foreach(IGrouping<Subject, Grade> grouping in dd)
            {
                allGrades.Add(grouping.ToList());
                subjects.Add(grouping.Key);
            }
            ViewBag.Subjects=subjects;
            return View(allGrades);
        }

        // GET: StudentGrades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Grade grade = await _gradesRepository.GetGradeByIdDetailed(id.Value);

            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }
    }
}
