using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace DziennikUcznia.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class SchoolClass
    {
        public int Id { get; set; }
        
        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; } = "NO_NAME";

        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
