namespace SchoolData.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Teacher? Teacher { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Mark> Marks { get; set; } = new List<Mark>();
    }
}
