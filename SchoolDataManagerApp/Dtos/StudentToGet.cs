namespace SchoolDataManagerApp.Dtos
{
    /// <summary>
    /// Student data used for viewing
    /// </summary>
    public class StudentToGet
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
