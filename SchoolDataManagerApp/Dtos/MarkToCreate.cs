using System.ComponentModel.DataAnnotations;

namespace SchoolDataManagerApp.Dtos
{
    /// <summary>
    /// Mark data used for creation
    /// </summary>
    public class MarkToCreate
    {
        [Required]
        [Range(1, 10)]
        public int Value { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int StudentId { get; set; }
    }
}
