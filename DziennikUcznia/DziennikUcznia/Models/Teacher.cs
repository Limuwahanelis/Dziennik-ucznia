using DziennikUcznia.Identity;
using DziennikUcznia.Models.View_Models;

namespace DziennikUcznia.Models
{
    public class Teacher
    {
        public Teacher() { }
        public Teacher(CreateTeacherModel teacher)
        {
            FirstName = teacher.FirstName;
            LastName = teacher.LastName;
            Id = teacher.Id;
        }
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public AppUser UserId { get; set; }=null!;
        public ICollection<Grade> Grades { get; } = new List<Grade>();
        public ICollection<SchoolClass> Classes { get; set; } = new List<SchoolClass>();
    }
}
