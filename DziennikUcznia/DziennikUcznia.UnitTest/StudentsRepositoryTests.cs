using DziennikUcznia.Data;
using DziennikUcznia.Models;
using DziennikUcznia.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DziennikUcznia.UnitTest
{
    public class StudentsRepositoryTests : IDisposable
    {
        string connectionString;
        SchoolDbContext_SQLServer _context;
        public StudentsRepositoryTests()
        {
            connectionString = $"Server=(localdb)\\mssqllocaldb;Database=DziennikTestDB{Guid.NewGuid()};Trusted_Connection=True;";
            _context = new SchoolDbContext_SQLServer(new DbContextOptionsBuilder<SchoolDBContext>()
           .UseSqlServer(connectionString).Options);
            _context.Database.EnsureCreated();
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
        [Fact]
        public async void RemoveStudent_ReturnsListOfStudentWithSize1()
        {
            Student student1 = new Student() { FirstName = "ADAM", LastName = "Nowak;" };
            Student student2 = new Student() { FirstName = "Darek", LastName = "Wilk" };
            _context.Students.Add(student1);
            _context.Students.Add(student2);
            StudentsRepository repository = new StudentsRepository(_context);
            await repository.RemoveStudent(student1);
            List<Student> students = await _context.Students.ToListAsync();
            Assert.Single(students);
            //_context.Students.a
        }
        [Fact]
        public async void GetStudents_ReturnsListOfStudentWithSize3()
        {
            Student student1 = new Student() { FirstName = "ADAM", LastName = "Nowak;" };
            Student student2 = new Student() { FirstName = "Darek", LastName = "Wilk" };
            Student student3 = new Student() { FirstName = "Tomek", LastName = "Dud;" };
            _context.Students.Add(student1);
            _context.Students.Add(student2);
            _context.Students.Add(student3);
            _context.SaveChanges();
            StudentsRepository repository = new StudentsRepository(_context);
            List<Student> students = await repository.GetStudents();
            Assert.Equal(3, students.Count);
            //_context.Students.a
        }
        [Fact]
        public async void GetStudentWithGradesById_CheckForStudnetWithId2_ReturnGrade3AndStudentADAM()
        {
            List<Grade> grades = new List<Grade>() { new Grade() {Value=3,Type=Grade.GradeType.HOMEWORK },
            new Grade(){ Value=1,Type=Grade.GradeType.ACTIVITY},
            new Grade(){Value=2,Type=Grade.GradeType.EGZAM } };
            Student student1 = new Student() { FirstName = "ADAM", LastName = "Nowak;" };
            Student student2 = new Student() { FirstName = "Darek", LastName = "Wilk" };
            Student student3 = new Student() { FirstName = "Tomek", LastName = "Dud;" };
            Teacher teacher = new Teacher() { FirstName = "Anna", LastName = "Fizyka" };
            grades[0].Student = student1;
            grades[1].Student = student2;
            grades[2].Student = student3;
            _context.Students.Add(student2);
            _context.Students.Add(student1);
            _context.Students.Add(student3);
            _context.Teachers.Add(teacher);
            foreach (Grade grade in grades)
            {
                grade.Teacher = teacher;
                _context.Grades.Add(grade);
            }
            _context.SaveChanges();
            StudentsRepository repository = new StudentsRepository(_context);
            Student student = await repository.GetStudentWithGradesById(2);
            List<Grade> studentGrades = new List<Grade>(student.Grades);
            Assert.Equal("ADAM", student.FirstName);
            Assert.Equal(3, studentGrades[0].Value);
            //_context.Students.a
        }
        [Fact]
        public async void GetStudentById_TryId2_ReturnsStudentWithNameTomek()
        {
            List<Student> students = new List<Student>() {
                new Student() { FirstName = "ADAM", LastName = "Nowak;" } ,
                new Student() { FirstName = "Tomek", LastName = "Dud;" },
                new Student() {  FirstName = "Darek", LastName = "Wilk" }
        };
            await _context.Students.AddRangeAsync(students);
            _context.SaveChanges();
            StudentsRepository repository=new StudentsRepository( _context);
            Student st = await repository.GetStudentById(2);
            Assert.Equal("Tomek", st.FirstName);
        }
    }
}
