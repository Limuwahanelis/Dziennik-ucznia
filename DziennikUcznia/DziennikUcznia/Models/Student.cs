using DziennikUcznia.Models.View_Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DziennikUcznia.Models
{
    public class Student
    {
        public Student() { }
        public Student(CreateStudentModel model)
        {
            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;
        }
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Class? Class { get; set; }
        public ICollection<Grade> Grades { get; } = new List<Grade>();

    }
}
