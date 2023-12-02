using System.ComponentModel.DataAnnotations;

namespace SchoolDataManagerApp.Dtos
{
    /// <summary>
    /// Teacher data used for updating
    /// </summary>
    public class TeacherToUpdate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}