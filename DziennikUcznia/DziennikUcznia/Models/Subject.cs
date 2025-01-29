using Microsoft.EntityFrameworkCore;

namespace DziennikUcznia.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Subject
    {
        public Subject() { }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}

