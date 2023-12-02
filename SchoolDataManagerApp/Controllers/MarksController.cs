using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolData;
using SchoolData.Models;
using SchoolDataManagerApp.Dtos;
using SchoolDataManagerApp.Extensions;
using SchoolManagerApp.Extensions;

namespace SchoolDataManagerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        /// <summary>
        /// Creates a new subject
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPost("add-subject")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        public IActionResult AddSubject([FromBody] SubjectToCreate subject)
        {
            using var ctx = new SchoolDataDbContext();

            ctx.Subjects.Add(new Subject
            {
                Name = subject.Name
            });
            ctx.SaveChanges();

            return Ok("Subject created successfully");
        }

        /// <summary>
        /// Enroll a student to a subject
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="subjectId"></param>
        [HttpPost("{studentId}/{subjectId}/enroll-student")]
        public void EnrollStudent(int studentId, int subjectId)
        {
            using var ctx = new SchoolDataDbContext();

            var student = ctx.Students.FirstOrDefault(s => s.Id == studentId);
            var subject = ctx.Subjects.FirstOrDefault(s => s.Id == subjectId);

            student.Subjects.Add(subject);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Assign a mark
        /// </summary>
        /// <param name="mark"></param>
        /// <returns></returns>
        [HttpPost("add-mark")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        public IActionResult AddMark([FromBody] MarkToCreate mark)
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

            return Ok("Mark assigned successfully");
        }

        /// <summary>
        /// Returns a list of marks given to a student
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet("{studentId}/get-marks-by-student")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MarkToGet>))]
        public IActionResult GetAllMarksByStudent(int studentId)
        {
            using var ctx = new SchoolDataDbContext();

            if (studentId == 0)
            {
                return BadRequest("Invalid id.");
            }

            var marks = ctx.Marks.
                Where(m => m.StudentId == studentId).
                Select(m => m.ToDto()).
                ToList();
            return Ok(marks);
        }

        /// <summary>
        /// Returns a list of all marks given to a student by subject
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        [HttpGet("{studentId}/{subjectId}/get-marks-by-student-and-subject")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MarkToGet>))]
        public IActionResult GetAllMarksByStudentBySubject(int studentId, int subjectId)
        {
            using var ctx = new SchoolDataDbContext();

            if (studentId == 0)
            {
                return BadRequest("Invalid id.");
            }

            var marks = ctx.Marks.
                Where(m => m.StudentId == studentId 
                        && m.SubjectId == subjectId).
                Select(m => m.ToDto()).
                ToList();
            return Ok(marks);
        }

        // Get Avg by Subject to giving Student
        //[HttpGet("{studentId}/get-average-of-marks")]
        //public void GetAvgOfMarks(int studentId)
        //{
        //    using var ctx = new SchoolDataDbContext();


        //}

        /// <summary>
        /// Returns a list of all students and their average of all of their marks
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
        /// Deletes a student and all of their marks
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

            ctx.Remove(ctx.Students.FirstOrDefault(s => s.Id == studentId));

            ctx.SaveChanges();

            return Ok("Student deleted successfully.");
        }
    }
}
