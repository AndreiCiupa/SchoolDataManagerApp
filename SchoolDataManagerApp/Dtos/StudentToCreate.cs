using System.ComponentModel.DataAnnotations;

namespace SchoolDataManagerApp.Dtos
{
    /// <summary>
    /// Student data used for creation
    /// </summary>
    public class StudentToCreate
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Range(6, 120)]
        public int Age { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
