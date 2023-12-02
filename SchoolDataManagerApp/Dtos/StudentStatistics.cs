namespace SchoolDataManagerApp.Dtos
{
    /// <summary>
    /// Student data used for viewing student statistics
    /// </summary>
    public class StudentStatistics
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Average { get; set; }
    }
}
