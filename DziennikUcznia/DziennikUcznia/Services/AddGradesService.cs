﻿using DziennikUcznia.Identity;
using DziennikUcznia.Models.View_Models;
using DziennikUcznia.Models;
using DziennikUcznia.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DziennikUcznia.Interfaces.Repositories;
using NuGet.Protocol.Core.Types;
using DziennikUcznia.Interfaces.Services;

namespace DziennikUcznia.Services
{
    public class AddGradesService: IAddGradesService
    {
        private StudentsRepository _studentsRepository;
        private UserManager<AppUser> _userManager;
        private TeachersRepository _teachersRepository;
        private GradesRepository _gradesRepository;
        public AddGradesService( StudentsRepository studentsRepository, GradesRepository gradesRepository,
             UserManager<AppUser> userManager, TeachersRepository teachersRepository)
        {
            _studentsRepository = studentsRepository;
            _gradesRepository = gradesRepository;
            _userManager = userManager;
            _teachersRepository = teachersRepository;
        }
        public async Task<bool> AddGrade(int studentId, AddGradeModel modelGrade, string tacherAppUserId)
        {

            var student = await _studentsRepository.GetStudentById(studentId);
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser user = await _userManager.FindByIdAsync(tacherAppUserId);
            var teacher = await _teachersRepository.GetTeacherByAppUser(user);
            if (student == null)
            {
                return false;
            }
            Grade grade = new Grade(modelGrade);
            grade.Student = student;
            grade.Teacher = teacher;
            await _gradesRepository.AddGrade(grade);
            return true;
        }
    }
}
