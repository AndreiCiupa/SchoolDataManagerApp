using System.ComponentModel.DataAnnotations;

namespace SchoolData.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Range(6, 120)]
        public int Age { get; set; }
        public string Address { get; set; }

        public List<Mark> Marks { get; set; } = new List<Mark>();
        public List<Subject> Subjects { get; set; } = new List<Subject>();

    }
}
