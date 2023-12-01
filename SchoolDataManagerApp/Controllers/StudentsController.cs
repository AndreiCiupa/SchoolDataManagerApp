using Microsoft.AspNetCore.Mvc;
using SchoolData;
using SchoolData.Models;

namespace SchoolDataManagerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // Get All Students
        [HttpGet("get-all-students")]
        public List<Student> GetAllStudents()
        {
            using var ctx = new SchoolDataDbContext();

            return ctx.Students.ToList();
        }

        // Get Student
        [HttpGet("{studentId}/get-student")]
        public Student GetStudent(int studentId) 
        {
            using var ctx = new SchoolDataDbContext();

            return ctx.Students.FirstOrDefault(s => s.Id == studentId);
        }

        // Create Student
        // !!!!!!
        // Add subject
        // !!!!!!
        [HttpPost("add-student")]
        public void AddStudent([FromBody] Student student)
        {
            using var ctx = new SchoolDataDbContext();

            ctx.Students.Add(student);
            ctx.SaveChanges();
        }

        // Change Student Data
        [HttpPost("{studentId}/change-student-data")]
        public void ChangeStudentData(int studentId, [FromBody] Student newStudent)
        {
            using var ctx = new SchoolDataDbContext();

            var student = ctx.Students.FirstOrDefault(s => s.Id == studentId);

            student = newStudent;
            ctx.SaveChanges();
        }
    }
}
