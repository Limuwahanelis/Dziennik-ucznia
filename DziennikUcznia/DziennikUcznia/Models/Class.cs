using System.Collections.ObjectModel;

namespace DziennikUcznia.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; } = "NO_NAME";

        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
