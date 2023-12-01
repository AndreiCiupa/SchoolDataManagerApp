using System.ComponentModel.DataAnnotations;

namespace SchoolDataManagerApp.Dtos
{
    public class TeacherToUpdate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}