using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolData;
using SchoolData.Models;
using SchoolDataManagerApp.Dtos;
using SchoolDataManagerApp.Extensions;
using SchoolManagerApp.Extensions;
using System.Reflection.Metadata.Ecma335;

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("student-statistics")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(List<StudentStatistics>))]
        public IActionResult GetStudentsStatistics()
        {
            using var ctx = new SchoolDataDbContext();

            var students = ctx.Students.
                Include(s => s.Marks).
                ToList();

            return Ok(
                students.Select(s => 
                {
                    return new StudentStatistics
                    {
                        Id = s.Id,
                        Name = s.FirstName + " " + s.LastName,
                        Age = s.Age,
                        Average = s.Marks.Average(n => n.Value)
                    };
                } ).ToList()
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpDelete("{studentId}/delete-student")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult DeleteStudent(int studentId)
        {
            using var ctx = new SchoolDataDbContext();

            if(studentId == 0)
            {
                return BadRequest("Invalid id.");
            }

            var student = ctx.Students.FirstOrDefault(s => s.Id == studentId);

            if (!student.Marks.IsNullOrEmpty())
            {
                ctx.Remove(ctx.Marks.Where(m => m.StudentId == studentId).ToList());
            }

            //ctx.Remove(ctx.Marks.Where(m => m.StudentId == studentId).ToList());
            ctx.Remove(ctx.Students.FirstOrDefault(s => s.Id == studentId));

            ctx.SaveChanges();

            return Ok("Student deleted successfully.");
        }
    }
}
