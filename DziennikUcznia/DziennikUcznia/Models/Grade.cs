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
        public Student Student { get; set; }
        public int Value { get; set; }
        // public int SubjectId { get; set; }
        // public int TeacherId { get; set; }
        [Column("Grade_type")]
        public GradeType Type { get; set;}
    }
}
