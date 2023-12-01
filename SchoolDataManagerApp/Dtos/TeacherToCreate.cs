using SchoolData.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolDataManagerApp.Dtos
{
    public class TeacherToCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public Rank Rank { get; set; } = new Rank();
        [Required]
        public int? SubjectId { get; set; }
    }
}
