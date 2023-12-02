using System.ComponentModel.DataAnnotations;

namespace SchoolDataManagerApp.Dtos
{
    /// <summary>
    /// Subject data used for creation
    /// </summary>
    public class SubjectToCreate
    {
        [Required]
        public string Name { get; set; }
    }
}
