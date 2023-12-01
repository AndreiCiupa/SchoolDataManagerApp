using System.ComponentModel.DataAnnotations;

namespace SchoolDataManagerApp.Dtos
{
    public class StudentToGet
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
