using Microsoft.AspNetCore.Mvc;
using SchoolData.Models;
using SchoolData;

namespace SchoolDataManagerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedDbController : ControllerBase
    {
        [HttpPost("seed-database")]
        public void SeedDb()
        {
            using var ctx = new SchoolDataDbContext();

            // Teacher
            var teacher = new Teacher
            {
                Name = "Dorin Gal",
                Address = "Oradea, Str Mestesugarilor, Nr 1",
                Rank = Rank.Instructor
            };
            //Teacher added to Db
            ctx.Add(teacher);

            // Subject
            var subject = new Subject
            {
                Name = "Biology"
            };
            // Subject added to Db
            ctx.Add(subject);

            // Subject allocated to teacher
            teacher.Subject = subject;
            subject.Teacher = teacher;

            // Students
            var s1 = new Student
            {
                FirstName = "Larisa",
                LastName = "Buzle",
                Age = 16,
                Address = "Oradea, str Calea Aradaului, nr 1"
            };
            var s2 = new Student
            {
                FirstName = "Ion",
                LastName = "Bucsa",
                Age = 16,
                Address = "Palota, str Principala, nr 1"
            };
            var s3 = new Student
            {
                FirstName = "Alex",
                LastName = "Borsa",
                Age = 16,
                Address = "Oradea, str Calea Aradaului, nr 2"
            };
            var s4 = new Student
            {
                FirstName = "Radu",
                LastName = "Irimie",
                Age = 17,
                Address = "Palota, str Estului, nr 1"
            };
            var s5 = new Student
            {
                FirstName = "Amalia",
                LastName = "Tatar",
                Age = 17,
                Address = "Alesd, str Florilor, nr 1"
            };

            // Add students to Db
            ctx.Add(s1);
            ctx.Add(s2);
            ctx.Add(s3);
            ctx.Add(s4);
            ctx.Add(s5);

            // Students - Subjects
            s1.Subjects.Add(subject);
            s2.Subjects.Add(subject);
            s3.Subjects.Add(subject);
            s4.Subjects.Add(subject);
            s5.Subjects.Add(subject);

            subject.Students.Add(s1);
            subject.Students.Add(s2);
            subject.Students.Add(s3);
            subject.Students.Add(s4);
            subject.Students.Add(s5);

            // Marks
            var mark1S1 = new Mark
            {
                Value = 10,
                DateTime = DateTime.Now,
                Subject = subject,
                Student = s1
            };
            var mark2S1 = new Mark
            {
                Value = 9,
                DateTime = DateTime.Now,
                Subject = subject,
                Student = s1
            };
            var markS2 = new Mark
            {
                Value = 10,
                DateTime = DateTime.Now,
                Subject = subject,
                Student = s2
            };
            var markS3 = new Mark
            {
                Value = 8,
                DateTime = DateTime.Now,
                Subject = subject,
                Student = s3
            };
            var markS4 = new Mark
            {
                Value = 10,
                DateTime = DateTime.Now,
                Subject = subject,
                Student = s4
            };
            var markS5 = new Mark
            {
                Value = 6,
                DateTime = DateTime.Now,
                Subject = subject,
                Student = s5
            };

            // Add the marks to students
            s1.Marks.Add(mark1S1);
            s1.Marks.Add(mark2S1);
            s2.Marks.Add(markS2);
            s3.Marks.Add(markS3);
            s4.Marks.Add(markS4);
            s5.Marks.Add(markS5);

            ctx.SaveChanges();
        }
    }
}
