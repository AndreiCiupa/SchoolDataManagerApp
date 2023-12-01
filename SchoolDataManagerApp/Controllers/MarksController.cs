using Microsoft.AspNetCore.Mvc;
using SchoolData;
using SchoolData.Models;
using SchoolDataManagerApp.Dtos;
using SchoolDataManagerApp.Extensions;

namespace SchoolDataManagerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        // Add Subject
        [HttpPost("add-subject")]
        public void AddSubject([FromBody] SubjectToCreate subject)
        {
            using var ctx = new SchoolDataDbContext();

            ctx.Subjects.Add(new Subject
            {
                Name = subject.Name
            });
            ctx.SaveChanges();
        }

        // Add Mark to Student
        [HttpPost("add-mark")]
        public void AddMark([FromBody] MarkToCreate mark)
        {
            using var ctx = new SchoolDataDbContext();

            ctx.Marks.Add(new Mark
            {
                Value = mark.Value,
                DateTime = DateTime.Now,
                StudentId = mark.StudentId,
                SubjectId = mark.SubjectId
            });
            ctx.SaveChanges();
        }

        // Get All Marks by a Student
        [HttpGet("{studentId}/get-marks-by-student")]
        public List<MarkToGet> GetAllMarksByStudent(int studentId)
        {
            using var ctx = new SchoolDataDbContext();

            var marks = ctx.Marks.
                Where(m => m.StudentId == studentId).
                Select(m => m.ToDto()).
                ToList();
            return marks;
        }

        // Get All Marks by a Student to a giving Subject
        [HttpGet("{studentId}/{subjectId}/get-marks-by-student-and-subject")]
        public List<MarkToGet> GetAllMarksByStudentBySubject(int studentId, int subjectId)
        {
            using var ctx = new SchoolDataDbContext();

            var marks = ctx.Marks.
                Where(m => m.StudentId == studentId 
                        && m.SubjectId == subjectId).
                Select(m => m.ToDto()).
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

            ctx.Remove(ctx.Marks.Where(m => m.StudentId == studentId).ToList());
            ctx.Remove(ctx.Students.FirstOrDefault(s => s.Id == studentId));

            ctx.SaveChanges();
        }
    }
}
