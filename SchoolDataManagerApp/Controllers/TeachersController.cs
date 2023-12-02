using Microsoft.AspNetCore.Mvc;
using SchoolData.Models;
using SchoolData;
using SchoolDataManagerApp.Dtos;
using SchoolDataManagerApp.Extensions;

namespace SchoolDataManagerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        /// <summary>
        /// Deletes a subject and its teacher if any
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        [HttpDelete("{subjectId}/delete-subject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult DeleteSubject(int subjectId)
        {
            using var ctx = new SchoolDataDbContext();

            if (subjectId == 0)
            {
                return BadRequest("Invalid id.");
            }

            var subject = ctx.Subjects.FirstOrDefault(s => s.Id == subjectId);

            if(subject.Teacher != null)
            {
                ctx.Remove(ctx.Teachers.FirstOrDefault(t => t.SubjectId == subjectId));
            }

            ctx.Remove(ctx.Subjects.FirstOrDefault(s => s.Id == subjectId));

            ctx.SaveChanges();
            return Ok("Student deleted successfully.");
        }

        /// <summary>
        /// Creates a new teacher
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPost("add-teacher")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        public IActionResult AddTeacher([FromBody] TeacherToCreate teacher)
        {
            using var ctx = new SchoolDataDbContext();

            ctx.Teachers.Add(new Teacher
            {
                Name = teacher.Name,
                Address = teacher.Address,
                Rank = teacher.Rank,
                SubjectId = teacher.SubjectId
            });
            ctx.SaveChanges();

            return Created("Teacher created successfully", true);
        }

        /// <summary>
        /// Deletes a teacher by Id
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        [HttpDelete("{teacherId}/delete-teacher")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult DeleteTeacher(int teacherId)
        {
            using var ctx = new SchoolDataDbContext();

            if (teacherId == 0)
            {
                return BadRequest("Invalid id.");
            }

            ctx.Remove(ctx.Teachers.FirstOrDefault(t => t.SubjectId == teacherId));

            ctx.SaveChanges();
            return Ok("Student deleted successfully.");
        }

        /// <summary>
        /// Updates teacher info
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="newTeacher"></param>
        /// <returns></returns>
        [HttpPost("{teacherId}/change-teacher-data")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult ChangeTeacherData(int teacherId, [FromBody] TeacherToUpdate newTeacher)
        {
            using var ctx = new SchoolDataDbContext();

            if (teacherId == 0)
            {
                return BadRequest("Invalid id.");
            }

            var teacher = ctx.Teachers.FirstOrDefault(t => t.Id == teacherId);
            if(teacher != null)
            {
                teacher.Name = newTeacher.Name;
                teacher.Address = newTeacher.Address;
            }
            
            ctx.SaveChanges();

            return Created("Teacher info changed successfully", true);
        }

        /// <summary>
        /// Assign a Subject to a Teacher
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="subjectId"></param>
        [HttpPost("{teacherId}/{subjectId}/add-subject-to-teacher")]
        public void AssignSubjectToTeacher(int teacherId, int subjectId)
        {
            using var ctx = new SchoolDataDbContext();

            var teacher = ctx.Teachers.FirstOrDefault(t => t.Id == teacherId);
            var subject = ctx.Subjects.FirstOrDefault(s => s.Id == subjectId);
            
            if(subject.Teacher == null)
            {
                subject.Teacher = teacher;
            }
            
            ctx.SaveChanges();
        }

        /// <summary>
        /// Promotes a teacher
        /// </summary>
        /// <param name="teacherId"></param>
        [HttpPost("{teacherId}/promote-teacher")]
        public void PromoteTeacher(int teacherId)
        {
            using var ctx = new SchoolDataDbContext();

            var teacher = ctx.Teachers.FirstOrDefault(t => t.Id == teacherId);

            if (teacher.Id != null)
            {
                switch (teacher.Rank)
                {
                    case Rank.Instructor:
                        teacher.Rank = Rank.AssistantProfessor;
                        break;
                    case Rank.AssistantProfessor:
                        teacher.Rank = Rank.AssociateProfessor;
                        break;
                    case Rank.AssociateProfessor:
                        teacher.Rank = Rank.Professor;
                        break;
                    default: break;
                }
            }

            ctx.SaveChanges();
        }

        /// <summary>
        /// Returns a list of all marks given by a teacher
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        [HttpGet("{teacherId}/marks-given-by-teacher")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MarkToGet>))]
        public IActionResult GetAllMarks(int teacherId)
        {
            using var ctx = new SchoolDataDbContext();

            if (teacherId == 0)
            {
                return BadRequest("Invalid id.");
            }

            return Ok(ctx.Marks.
                Where(m => m.Subject.Teacher.Id == teacherId).
                Select(m => m.ToDto()).
                ToList());
        }
    }
}
