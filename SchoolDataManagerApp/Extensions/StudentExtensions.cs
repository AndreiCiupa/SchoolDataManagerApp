using SchoolData.Models;
using SchoolDataManagerApp.Dtos;

namespace SchoolManagerApp.Extensions
{
    public static class StudentExtensions
    {
        public static StudentToGet ToDto(this Student student)
        {
            if (student == null)
            {
                return null;
            }

            return new StudentToGet
            {
                Id = student.Id,
                LastName = student.LastName,
                FirstName = student.FirstName,
                Age = student.Age,
                Address = student.Address
            };
        }

        public static Student ToEntity(this StudentToCreate student)
        {
            if (student == null)
            {
                return null;
            }

            return new Student
            {
                LastName = student.LastName,
                FirstName = student.FirstName,
                Age = student.Age,
                Address = student.Address
            };
        }
    }
}
