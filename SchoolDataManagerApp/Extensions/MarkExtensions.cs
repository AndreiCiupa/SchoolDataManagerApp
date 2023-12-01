using SchoolData.Models;
using SchoolDataManagerApp.Dtos;

namespace SchoolDataManagerApp.Extensions
{
    public static class MarkExtensions
    {
        public static MarkToGet ToDto(this Mark mark)
        {
            if (mark == null)
            {
                return null;
            }

            return new MarkToGet
            {
                Value = mark.Value,
                DateTime = mark.DateTime,
                StudentId = mark.StudentId,
            };
        }
    }
}
