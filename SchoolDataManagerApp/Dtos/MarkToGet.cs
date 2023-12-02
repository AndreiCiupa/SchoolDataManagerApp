namespace SchoolDataManagerApp.Dtos
{
    /// <summary>
    /// Mark data used for viewing
    /// </summary>
    public class MarkToGet
    {
        public int Value { get; set; }
        public DateTime DateTime { get; set; }
        public int StudentId { get; set; }
    }
}
