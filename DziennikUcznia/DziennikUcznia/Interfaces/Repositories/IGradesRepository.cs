﻿using DziennikUcznia.Data;
using DziennikUcznia.Models;
using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Interfaces.Repositories
{
    public interface IGradesRepository
    {
        public Task AddGrade(Grade grade);

        public Task<List<Grade>> GetGradesByStudent(Student student);

        public Task<Grade> GetGradeById(int id);

        public Task<List<IGrouping<Subject, Grade>>> GetStudentGradesGroupedBySubject(Student student);
        public Task<Grade> GetGradeByIdDetailed(int id);
    }
}
