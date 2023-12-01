using Microsoft.AspNetCore.Mvc;
using SchoolData.Models;
using SchoolData;

namespace SchoolDataManagerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        // Deletes Subject 


        // Add Teacher
        [HttpPost("add-teacher")]
        public void AddTeacher([FromBody] Teacher teacher)
        {
            using var ctx = new SchoolDataDbContext();

            ctx.Teachers.Add(teacher);
            ctx.SaveChanges();
        }

        // Delete Teacher
        // Subject remains as it could function as a job opening


        // Change Teacher Data
        [HttpPost("{teacherId}/change-teacher-data")]
        public void ChangeTeacherData(int teacherId, [FromBody] Teacher newTeacher)
        {
            using var ctx = new SchoolDataDbContext();

            var teacher = ctx.Teachers.FirstOrDefault(t => t.Id == teacherId);
            if(teacher != null)
            {
                teacher = newTeacher;
            }
            
            ctx.SaveChanges();
        }

        // Add A Subject to a Teacher
        [HttpPost("{teacherId}/{subjectId}/add-subject-to-teacher")]
        public void AddSubjectToTeacher(int teacherId, int subjectId)
        {
            using var ctx = new SchoolDataDbContext();

            var teacher = ctx.Teachers.FirstOrDefault(t => t.Id == teacherId);
            var subject = ctx.Subjects.FirstOrDefault(s => s.Id == subjectId);
            
            if(subject.Teacher == null && teacher.Subject == null)
            {
                teacher.Subject = subject;
                subject.Teacher = teacher;
            }
            
            ctx.SaveChanges();
        }

        // Promote Teacher
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

        // Get All Marks given by a specific Teacher


    }
}
