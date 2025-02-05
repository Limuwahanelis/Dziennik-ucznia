using static DziennikUcznia.Models.Grade;

namespace DziennikUcznia.Models.View_Models
{
    public class AddGradeModel
    {
        public int Value { get; set; }
        public GradeType Type { get; set; }

        public int SubjectId { get; set; }
    }
}
