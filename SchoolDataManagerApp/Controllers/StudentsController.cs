using Microsoft.AspNetCore.Mvc;
using SchoolData;
using SchoolDataManagerApp.Dtos;
using SchoolManagerApp.Extensions;

namespace SchoolDataManagerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        /// <summary>
        /// Returns a list of all students
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-students")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentToGet>))]
        public IActionResult GetAllStudents()
        {
            using var ctx = new SchoolDataDbContext();

            return Ok(ctx.Students.
                Select(s => s.ToDto()).
                ToList());         
        }

        /// <summary>
        /// Returns student by Id
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet("{studentId}/get-student")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToGet))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetStudent(int studentId) 
        {
            using var ctx = new SchoolDataDbContext();

            if (studentId == 0)
            {
                return BadRequest("Invalid id.");
            }

            return Ok(ctx.Students.
                FirstOrDefault(s => s.Id == studentId).
                ToDto());
        }

       /// <summary>
       /// Creates a new student
       /// </summary>
       /// <param name="student"></param>
       /// <returns></returns>
        [HttpPost("add-student")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        public IActionResult AddStudent([FromBody] StudentToCreate student)
        {
            using var ctx = new SchoolDataDbContext();

            ctx.Students.Add(student.ToEntity());
            ctx.SaveChanges();

            return Created("Student added successfully", true);
        }

        /// <summary>
        /// Updates student info
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="newStudent"></param>
        /// <returns></returns>
        [HttpPost("{studentId}/change-student-data")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult ChangeStudentData(int studentId, [FromBody] StudentToCreate newStudent)
        {
            using var ctx = new SchoolDataDbContext();

            if (studentId == 0)
            {
                return BadRequest("Invalid id.");
            }

            var student = ctx.Students.FirstOrDefault(s => s.Id == studentId);

            student = newStudent.ToEntity();
            ctx.SaveChanges();

            return Created("Student info changed successfully", true);
        }
    }
}
