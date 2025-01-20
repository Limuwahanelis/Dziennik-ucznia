using System.ComponentModel.DataAnnotations.Schema;

namespace DziennikUcznia.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public IList<Grade>? Grades { get; set; }

    }
}
