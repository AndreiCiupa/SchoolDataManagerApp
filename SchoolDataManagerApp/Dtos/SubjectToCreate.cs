using System.ComponentModel.DataAnnotations;

namespace SchoolDataManagerApp.Dtos
{
    public class SubjectToCreate
    {
        [Required]
        public string Name { get; set; }
    }
}
