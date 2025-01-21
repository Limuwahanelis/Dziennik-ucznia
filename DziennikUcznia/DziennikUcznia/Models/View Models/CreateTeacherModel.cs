using System.ComponentModel;

namespace DziennikUcznia.Models.View_Models
{
    public class CreateTeacherModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public List<int> GradesId { get; set; } = new List<int>();
        [DisplayName("Classes")]
        public List<int> ClassesId { get; set; }= new List<int>();
    }
}
