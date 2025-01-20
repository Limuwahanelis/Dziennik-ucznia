using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DziennikUcznia.Models
{
    public class Grade
    {
        public enum GradeType
        {
            HOMEWORK,TEST,ACTIVITY,EGZAM
        }
        public int Id { get; set; }
        public Student Teacher { get; set; } = null!;
        public Student Student { get; set; } = null!;
        [Range(1,6)]
        public int Value { get; set; }
        [Column("Grade_type")]
        public GradeType Type { get; set;}
    }
}
