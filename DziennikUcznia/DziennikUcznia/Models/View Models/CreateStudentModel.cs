namespace DziennikUcznia.Models.View_Models
{
    public class CreateStudentModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? ClassId { get; set; }
    }
}
