﻿using DziennikUcznia.Data;
using DziennikUcznia.Identity;
using DziennikUcznia.Interfaces.Repositories;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Repositories
{
    public class StudentsRepository:IStudentsRepository
    {
        private readonly SchoolDBContext _context;

        public StudentsRepository(SchoolDBContext context)
        {
            _context = context;
        }
        public async Task AddStudent(Student student)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStudent(Student student)
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveStudent(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Student>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }
        public async Task<Student?> GetStudentWithGradesById(int id)
        {
            var student = await _context.Students
                .Include(s => s.Grades)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            return student;
        }
        public async Task<Student?> GetStudentById(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        public Task<Student?> GetStudentByAppUser(AppUser appUser)
        {
            return _context.Students.Where(st=>st.UserId==appUser).FirstOrDefaultAsync();
        }
    }
}
