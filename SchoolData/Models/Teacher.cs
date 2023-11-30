namespace SchoolData.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Rank Rank { get; set; } = new Rank();
        public int? SubjectId { get; set; }

        public Subject? Subject { get; set; }
    }
}
