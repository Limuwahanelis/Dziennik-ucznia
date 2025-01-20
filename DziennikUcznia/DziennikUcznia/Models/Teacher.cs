namespace DziennikUcznia.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<Grade> Grades { get; } = new List<Grade>();
        public ICollection<Class> Classes { get; set; } = new List<Class>();
    }
}
