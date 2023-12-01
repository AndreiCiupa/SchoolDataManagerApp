using Microsoft.AspNetCore.Mvc;
using SchoolData;
using SchoolData.Models;

namespace SchoolDataManagerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        // Add Subject
        [HttpPost("add-subject")]
        public void AddSubject([FromBody] Subject subject)
        {
            using var ctx = new SchoolDataDbContext();

            ctx.Subjects.Add(subject);
            ctx.SaveChanges();
        }

        // Add Mark to Student


        // Get All Marks by a Student
        [HttpGet("{studentId}/get-marks-by-student")]
        public List<Mark> GetAllMarksByStudent(int studentId)
        {
            using var ctx = new SchoolDataDbContext();

            var marks = ctx.Marks.
                Where(m => m.StudentId == studentId).
                ToList();
            return marks;
        }

        // Get All Marks by a Student to a giving Subject
        [HttpGet("{studentId}/{subjectId}/get-marks-by-student-and-subject")]
        public List<Mark> GetAllMarksByStudentBySubject(int studentId, int subjectId)
        {
            using var ctx = new SchoolDataDbContext();

            var marks = ctx.Marks.
                Where(m => m.StudentId == studentId 
                        && m.SubjectId == subjectId).
                ToList();
            return marks;
        }

        // Get Avg by Subject to giving Student
        [HttpGet("{studentId}/get-average-of-marks")]
        public void GetAvgOfMarks(int studentId)
        {
            using var ctx = new SchoolDataDbContext();


        }

        // Get List of Students by Avg of Marks


        // Delete a Student
        // What does that imply? 
        // Deleting all the marks given to that student
        [HttpDelete("{studentId}/delete-student")]
        public void DeleteStudent(int studentId)
        {
            using var ctx = new SchoolDataDbContext();

            ctx.Remove(ctx.Students.FirstOrDefault(s => s.Id == studentId));
            ctx.Remove(ctx.Marks.Where(m => m.StudentId == studentId).ToList());

            ctx.SaveChanges();
        }
    }
}
