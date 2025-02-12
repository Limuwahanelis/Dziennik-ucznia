using DziennikUcznia.Models.View_Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static DziennikUcznia.Models.Grade;
using System.ComponentModel;

namespace DziennikUcznia.Models
{
    public class DetailedGrade
    {
        public DetailedGrade() { }
        public DetailedGrade(Grade model)
        {
            Value = model.Value;
            Type = model.Type;
            TeacherName = model.Teacher.FirstName+" "+model.Teacher.LastName;
            Subject = model.Subject;
        }
        [DisplayName("Teacher")]
        public string TeacherName { get; set; }
        [Range(1, 6)]
        public int Value { get; set; }
        [Column("Grade_type")]
        public GradeType Type { get; set; }
        public Subject Subject { get; set; } = null!;
    }
}
