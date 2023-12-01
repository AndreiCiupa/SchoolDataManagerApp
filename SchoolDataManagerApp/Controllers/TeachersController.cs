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
        // Deletes Subject
        // also deletes teacher
        [HttpDelete("{subjectId}/delete-subject")]
        public void DeleteSubject(int subjectId)
        {
            using var ctx = new SchoolDataDbContext();

            ctx.Remove(ctx.Teachers.FirstOrDefault(t => t.SubjectId == subjectId));
            ctx.Remove(ctx.Subjects.FirstOrDefault(s => s.Id == subjectId));

            ctx.SaveChanges();
        }

        // Add Teacher
        [HttpPost("add-teacher")]
        public void AddTeacher([FromBody] TeacherToCreate teacher)
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
        }

        // Delete Teacher
        // Subject remains as it could function as a job opening
        [HttpDelete("{teacherId}/delete-teacher")]
        public void DeleteTeacher(int teacherId)
        {
            using var ctx = new SchoolDataDbContext();

            ctx.Remove(ctx.Teachers.FirstOrDefault(t => t.SubjectId == teacherId));

            ctx.SaveChanges();
        }

        // Change Teacher Data
        [HttpPost("{teacherId}/change-teacher-data")]
        public void ChangeTeacherData(int teacherId, [FromBody] TeacherToUpdate newTeacher)
        {
            using var ctx = new SchoolDataDbContext();

            var teacher = ctx.Teachers.FirstOrDefault(t => t.Id == teacherId);
            if(teacher != null)
            {
                teacher.Name = newTeacher.Name;
                teacher.Address = newTeacher.Address;
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
        [HttpGet("{teacherId}/marks-given-by-teacher")]
        public List<MarkToGet> GetAllMarks(int teacherId)
        {
            using var ctx = new SchoolDataDbContext();

            return ctx.Marks.
                Where(m => m.Subject.Teacher.Id == teacherId).
                Select(m => m.ToDto()).
                ToList();
        }
    }
}
